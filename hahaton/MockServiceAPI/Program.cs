using hahaton.Endpoints;
using MockServiceApplication;
using MockServiceApplication.FormatServices;
using MockServiceApplication.MockServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<Random>();
builder.Services.AddTransient<MockService>();

builder.Services.AddTransient<StringMockService>();
builder.Services.AddTransient<DoubleMockService>();
builder.Services.AddTransient<IntMockService>();
builder.Services.AddTransient<ArrayMockService>();

builder.Services.AddTransient<EmailFormatService>();
builder.Services.AddTransient<PhoneFormatService>();
builder.Services.AddTransient<DefaultMockService>();
builder.Services.AddTransient<DefaultFormatService>();
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
        "double" => serviceProvider.GetRequiredService<DoubleMockService>(),
        "int" => serviceProvider.GetRequiredService<IntMockService>(),
        _ => null
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapMockEndpoints();

app.Run();