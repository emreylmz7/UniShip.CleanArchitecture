//using Microsoft.AspNetCore.Identity;
//using UniShip.Domain.Users;
//using UniShip.Infrastructure.Context;
//using UniShip.Infrastructure.Seeds.Interfaces;

//namespace UniShip.Infrastructure.Seeds;
//public class RoleSeedData : BaseSeedData, IDataSeeder
//{
//    public int Order => 1; // İlk çalışacak seeder

//    public RoleSeedData(
//        ApplicationDbContext context,
//        UserManager<AppUser> userManager,
//        RoleManager<IdentityRole> roleManager)
//        : base(context, userManager, roleManager)
//    {
//    }

//    public async Task SeedAsync()
//    {
//        var roles = new[] { "Admin", "BranchManager", "CourierStaff", "CustomerService" };

//        foreach (var role in roles)
//        {
//            if (!await _roleManager.RoleExistsAsync(role))
//            {
//                await _roleManager.CreateAsync(new IdentityRole(role));
//            }
//        }
//    }
//}
