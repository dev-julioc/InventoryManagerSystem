using InventoryManagerSystem.Application.Dtos.Location;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.Application.Services.Interfaces;

public interface ILocationService
{
    Task<IEnumerable<ReadLocationDto>> GetAllLocationAsync();
    Task<ResultService<ReadLocationDto>> GetLocationByIdAsync(int id);
    Task<ResultService<ReadLocationDto>> AddLocationAsync(AddLocationDto locationDto);
    Task<ResultService> UpdateLocationAsync(UpdateLocationDto locationDto);
    Task<ResultService> DeleteLocationAsync(int id);
}