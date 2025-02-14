using Microsoft.AspNetCore.Identity;
using UniShip.Domain.Users;

namespace UniShip.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@uniship.com",
                    FirstName = "Emre",
                    LastName = "Yılmaz",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.Now,
                    Address = "Antalya"
                };

                user.CreatedBy = user.Id;

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }
}
