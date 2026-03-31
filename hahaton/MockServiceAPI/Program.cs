using hahaton.Endpoints;
using MockServiceApplication;
using MockServiceApplication.FormatServices;
using MockServiceApplication.MockServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<StringMockService>();
builder.Services.AddTransient<ArrayMockService<string>>();

builder.Services.AddTransient<EmailFormatService>();
builder.Services.AddTransient<DefaultFormatService>();

builder.Services.AddTransient<Func<Format, IFormatService<string>>>(serviceProvider => format =>
{
    return format switch
    {
        Format.Email => serviceProvider.GetRequiredService<EmailFormatService>(),
        _ => serviceProvider.GetRequiredService<DefaultFormatService>(),
    };
});

builder.Services.AddTransient<Func<string, IMockService<string>>>(serviceProvider => str =>
{
    return serviceProvider.GetRequiredService<StringMockService>();
});

builder.Services.AddTransient<Func<string, IMockService<Array>>>(serviceProvider => str =>
{
    return str switch
    {
        "string" => serviceProvider.GetRequiredService<ArrayMockService<string>>(),
        _ => throw new NotImplementedException(),
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapMockEndpoints();

app.Run();