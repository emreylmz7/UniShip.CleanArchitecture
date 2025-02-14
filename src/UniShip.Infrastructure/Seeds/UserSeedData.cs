using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniShip.Domain.Users;
using UniShip.Infrastructure.Context;
using UniShip.Infrastructure.Seeds.Interfaces;

namespace UniShip.Infrastructure.Seeds;
public class UserSeedData : BaseSeedData, IDataSeeder
{
    public int Order => 2;

    public UserSeedData(
        ApplicationDbContext context,
        UserManager<AppUser> userManager)
        : base(context, userManager)
    {
    }

    public async Task SeedAsync()
    {
        if (!_userManager.Users.Any(p => p.UserName == "admin"))
        {
            var branch = await _context.Branches.FirstOrDefaultAsync();

            var user = new AppUser
            {
                UserName = "admin",
                Email = "admin@uniship.com",
                FirstName = "Emre",
                LastName = "Yılmaz",
                EmailConfirmed = true,
                CreatedDate = DateTime.Now,
                Address = "Antalya",
                BranchId = branch!.Id,
            };

            var result = await _userManager.CreateAsync(user, "1");

            if (result.Succeeded)
            {
                branch.CreatedBy = user.Id;
                user.CreatedBy = user.Id;

                _context.Branches.Update(branch);
                await _context.SaveChangesAsync();
            }
        }
    }
}
