using Scalar.AspNetCore;
using UniShip.Infrastructure;
using UniShip.WebAPI.Modules;

namespace UniShip.WebAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ConfigureApplicationAsync(this WebApplication app)
    {
        // 1️ Veri tabanını başlat
        await app.Services.SeedDatabaseAsync();

        // 2️ Middleware'leri ekleyelim
        app.UseHttpsRedirection(); // HTTPS yönlendirmesi en başta olmalı
        app.UseResponseCompression();
        app.UseExceptionHandler();

        app.UseCors(x => x
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true));

        app.UseAuthentication();
        app.UseAuthorization();

        app.RegisterRoutes();

        // 3️ OpenAPI ve Scalar API'yi ekleyelim
        app.MapOpenApi();
        app.MapScalarApiReference();
        app.MapDefaultEndpoints();

        // 4️ Rate Limiting ve Controller'ları ekleyelim
        app.MapControllers().RequireRateLimiting("fixed");
    }
}
