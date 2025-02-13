namespace UniShip.Application.Features.Auth.Login;
public sealed record LoginCommandResponse
{
    public string AccessToken { get; init; } = default!;
}
