using InventoryManagerSystem.Application.Dtos.Location;
using InventoryManagerSystem.Domain.Entities;

namespace InventoryManagerSystem.Application.Map;

public static class LocationMapping
{
    public static Location ConvertToEntity(this AddLocationDto locationDto)
        => new (locationDto.Name);

    public static ReadLocationDto ConvertToDto(this Location location)
        => new(location.Id, location.Name!);

    public static IEnumerable<ReadLocationDto> ConvertToDtoList(this IEnumerable<Location> locations)
        => locations.Select(x => x.ConvertToDto());
}