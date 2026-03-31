using hahaton.Endpoints;
using Microsoft.AspNetCore.Mvc.Formatters;
using MockServiceApplication;
using MockServiceApplication.FormatServices;
using MockServiceApplication.MockServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<StringMockService>();
builder.Services.AddTransient<Func<Format, IFormatService<string>>>(serviceProvider => format =>
{
    return format switch
    {
        Format.Email => serviceProvider.GetRequiredService<EmailFormatService>(),
        default => 
    }
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapMockEndpoints();

app.Run();