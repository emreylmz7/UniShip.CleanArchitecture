namespace UniShip.Infrastructure.Seeds.Interfaces;
public interface IDataSeeder
{
    Task SeedAsync();
    int Order { get; }
}
