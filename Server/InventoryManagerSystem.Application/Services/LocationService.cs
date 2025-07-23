using InventoryManagerSystem.Application.Dtos.Location;
using InventoryManagerSystem.Application.Map;
using InventoryManagerSystem.Application.Services.Interfaces;
using InventoryManagerSystem.Application.Services.Result;
using InventoryManagerSystem.Domain.Interfaces;

namespace InventoryManagerSystem.Application.Services;

public class LocationService(ILocationRepository locationRepository) : ILocationService
{
    public async Task<IEnumerable<ReadLocationDto>> GetAllLocationAsync()
    {
        var result = await locationRepository.GetLocationsAsync();

        return result.ConvertToDtoList();
    }

    public async Task<ResultService<ReadLocationDto>> GetLocationByIdAsync(int id)
    {
        var result = await locationRepository.GetLocationByIdAsync(id);
        
        return result is null 
            ? ResultService.NotFound<ReadLocationDto>("Location not found.")
            : ResultService.Ok(result.ConvertToDto());
    }

    public async Task<ResultService<ReadLocationDto>> AddLocationAsync(AddLocationDto locationDto)
    {
        if(await locationRepository.IsLocationExistsAsync(locationDto.Name))
            return ResultService.Fail<ReadLocationDto>("Location already exists.");
        
        var location = locationDto.ConvertToEntity();
        
        var result = await locationRepository.AddLocationAsync(location);
        
        return result is null 
            ? ResultService.Fail<ReadLocationDto>("Failed to add location.")
            : ResultService.Ok(result.ConvertToDto());
    }

    public async Task<ResultService> UpdateLocationAsync(UpdateLocationDto locationDto)
    {
        var location = await locationRepository.GetLocationByIdAsync(locationDto.Id);
        
        if (location is null)
            return ResultService.NotFound("Location not found.");
        
        if (!location.Name!.Equals(locationDto.Name) && 
            await locationRepository.IsLocationExistsAsync(locationDto.Name))
            return ResultService.Fail("Location already exists.");
        
        location.Update(locationDto.Name);
        
        var result = await locationRepository.UpdateLocationAsync(location);
        
        return !result 
            ? ResultService.Fail("Failed to update location.")
            : ResultService.Ok("Location updated successfully.");
    }

    public async Task<ResultService> DeleteLocationAsync(int id)
    {
        var location = await locationRepository.GetLocationByIdAsync(id);
        
        if (location is null)
            return ResultService.NotFound("Location not found.");
        
        var result = await locationRepository.DeleteLocationAsync(location);
        
        return !result 
            ? ResultService.Fail("Failed to delete location.")
            : ResultService.Ok("Location deleted successfully.");
    }
}