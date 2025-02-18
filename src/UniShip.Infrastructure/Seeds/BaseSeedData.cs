using Microsoft.AspNetCore.Identity;
using UniShip.Domain.Users;
using UniShip.Infrastructure.Context;

namespace UniShip.Infrastructure.Seeds;
public abstract class BaseSeedData
{
    protected readonly ApplicationDbContext _context;
    protected readonly UserManager<AppUser> _userManager;
    protected readonly RoleManager<IdentityRole<Guid>> _roleManager;

    protected BaseSeedData(
        ApplicationDbContext context,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
}