using InventoryManagerSystem.Domain.Entities;

namespace InventoryManagerSystem.Domain.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetLocationsAsync();
    Task<Location?> GetLocationByIdAsync(int id);
    Task<bool> IsLocationExistsAsync(string name);
    Task<Location?> AddLocationAsync(Location location);
    Task<bool> UpdateLocationAsync(Location location);
    Task<bool> DeleteLocationAsync(Location location);
}