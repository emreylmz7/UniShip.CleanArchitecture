using Microsoft.AspNetCore.Identity;
using UniShip.Domain.Users;
using UniShip.Infrastructure.Context;

namespace UniShip.Infrastructure.Seeds;
public abstract class BaseSeedData
{
    protected readonly ApplicationDbContext _context;
    protected readonly UserManager<AppUser> _userManager;

    protected BaseSeedData(
        ApplicationDbContext context,
        UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
}