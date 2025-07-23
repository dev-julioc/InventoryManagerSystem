using InventoryManagerSystem.Domain.Entities;
using InventoryManagerSystem.Domain.Interfaces;
using InventoryManagerSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerSystem.Infra.Data.Repositories;

public class LocationRepository(AppDbContext context) : ILocationRepository
{
    public async Task<IEnumerable<Location>> GetLocationsAsync()
        => await context.Locations.AsNoTracking().ToListAsync();

    public async Task<Location?> GetLocationByIdAsync(int id)
        => await context.Locations.FindAsync(id);

    public async Task<bool> IsLocationExistsAsync(string name)
        => await context.Locations.AsNoTracking().AnyAsync(x => x.Name!.ToLower().Equals(name.ToLower()));

    public async Task<Location?> AddLocationAsync(Location location)
    {
        context.Locations.Add(location);
        var result = await context.SaveChangesAsync();
        return result > 0 ? location : null;
    }

    public async Task<bool> UpdateLocationAsync(Location location)
    {
        context.Locations.Update(location);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteLocationAsync(Location location)
    {
        context.Locations.Remove(location);
        return await context.SaveChangesAsync() > 0;
    }
}