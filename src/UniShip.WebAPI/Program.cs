using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;
using UniShip.Application;
using UniShip.Infrastructure;
using UniShip.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// 1? Servisleri ekleyelim
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.AddServiceDefaults();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// 2? Rate Limiting ayarlar�
builder.Services.AddRateLimiter(x =>
    x.AddFixedWindowLimiter("fixed", cfg =>
    {
        cfg.QueueLimit = 100;
        cfg.Window = TimeSpan.FromMinutes(1);
        cfg.PermitLimit = 10;
        cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    })
);

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

// ? Tek sat�rda t�m middleware'leri ve seed i�lemini �a��r�yoruz
await app.ConfigureApplicationAsync();

app.Run();
