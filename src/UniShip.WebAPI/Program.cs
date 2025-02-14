using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;
using UniShip.Application;
using UniShip.Infrastructure;
using UniShip.WebAPI.Middlewares;
using UniShip.WebAPI.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.AddServiceDefaults();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddOpenApi(); // OpenAPI servisini projeye ekliyoruz.

builder.Services.AddControllers();

builder.Services.AddRateLimiter(x =>
x.AddFixedWindowLimiter("fixed", cfg =>
{
    cfg.QueueLimit = 100;
    cfg.Window = TimeSpan.FromMinutes(1);
    cfg.PermitLimit = 10;
    cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
}));

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

app.MapOpenApi(); // OpenAPI servisini projeye ekliyoruz.
app.MapScalarApiReference(); // Scalar API Reference servisini projeye ekliyoruz.

app.MapDefaultEndpoints();

app.UseHttpsRedirection(); // Https yönlendirmesini aktif hale getiriyoruz.

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true));

app.RegisterRoutes();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();
app.UseExceptionHandler();

ExtensionsMiddleware.CreateFirstUser(app);

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
