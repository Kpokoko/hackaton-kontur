using System.Text;
using Application.Identity.Services;
using Domain;
using hahaton.Endpoints;
using Infrastructure;
using Microsoft.IdentityModel.Tokens;
using MockServiceApplication;
using MockServiceApplication.FormatServices;
using MockServiceApplication.Identity.JWT;
using MockServiceApplication.MockServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Format = MockServiceApplication.FormatServices.Format;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<JwtProvider>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<AuthService, AuthService>();
builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<Random>();
builder.Services.AddTransient<MockService>();

builder.Services.AddTransient<StringMockService>();
builder.Services.AddTransient<DoubleMockService>();
builder.Services.AddTransient<IntMockService>();
builder.Services.AddTransient<ArrayMockService>();
builder.Services.AddTransient<DictionaryMockService>();

builder.Services.AddTransient<EmailFormatService>();
builder.Services.AddTransient<PhoneFormatService>();
builder.Services.AddTransient<DefaultMockService>();
builder.Services.AddTransient<DefaultFormatService>();
builder.Services.AddTransient<DataTimeFormatService>();
builder.Services.AddTransient<DoubleFormatService>();
builder.Services.AddTransient<IntFormatService>();

builder.Services.AddTransient<Func<Format?, IFormatService>>(serviceProvider => format =>
{
    return format switch
    {
        Format.Email => serviceProvider.GetRequiredService<EmailFormatService>(),
        Format.Phone => serviceProvider.GetRequiredService<PhoneFormatService>(),
        Format.DateTime => serviceProvider.GetRequiredService<DataTimeFormatService>(),
        Format.Double => serviceProvider.GetRequiredService<DoubleFormatService>(),
        Format.Int => serviceProvider.GetRequiredService<IntFormatService>(),
        _ => serviceProvider.GetRequiredService<DefaultFormatService>(),
    };
});


builder.Services.AddTransient<Func<string, IMockService?>>(serviceProvider => type =>
{
    return type switch
    {
        "string" => serviceProvider.GetRequiredService<StringMockService>(),
        "array" => serviceProvider.GetRequiredService<ArrayMockService>(),
        "dictionary" => serviceProvider.GetRequiredService<DictionaryMockService>(),
        "double" => serviceProvider.GetRequiredService<DoubleMockService>(),
        "int" => serviceProvider.GetRequiredService<IntMockService>(),
        _ => null
    };
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new()
    {
        Description = "Введите JWT токен в формате: Bearer {ваш токен}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new()
    {
        {
            new() {
                Reference = new() {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapMockEndpoints();
app.MapAuthEndpoints();

app.Run();