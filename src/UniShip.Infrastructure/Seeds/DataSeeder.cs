using UniShip.Infrastructure.Seeds.Interfaces;

namespace UniShip.Infrastructure.Seeds;
public class DataSeeder
{
    private readonly IEnumerable<IDataSeeder> _seeders;

    public DataSeeder(IEnumerable<IDataSeeder> seeders)
    {
        _seeders = seeders;
    }

    public async Task SeedAsync()
    {
        foreach (var seeder in _seeders.OrderBy(s => s.Order))
        {
            await seeder.SeedAsync();
        }
    }
}
