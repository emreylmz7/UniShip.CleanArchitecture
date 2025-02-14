using Microsoft.AspNetCore.Identity;
using UniShip.Domain.Branchs;
using UniShip.Domain.Users;
using UniShip.Infrastructure.Context;
using UniShip.Infrastructure.Seeds.Interfaces;

namespace UniShip.Infrastructure.Seeds;
public class BranchSeedData : BaseSeedData, IDataSeeder
{
    public int Order => 1; 

    public BranchSeedData(
        ApplicationDbContext context,
        UserManager<AppUser> userManager)
        : base(context, userManager)
    {
    }

    public async Task SeedAsync()
    {
        if (!_context.Branches.Any())
        {
            var branch = new Branch
            {
                Name = "Antalya Merkez Şube",
                Address = "Antalya Merkez Mah. No:1",
                PhoneNumber = "02421234567",
                Email = "antalya.merkez@uniship.com",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
        }
    }
}
