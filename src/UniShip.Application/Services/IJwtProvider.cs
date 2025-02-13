using UniShip.Domain.Users;

namespace UniShip.Application.Services;
public interface IJwtProvider
{
    public Task<string> GenerateJwtToken(AppUser user, CancellationToken cancellationToken = default);
}
