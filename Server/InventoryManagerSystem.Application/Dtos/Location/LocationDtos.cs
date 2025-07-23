namespace InventoryManagerSystem.Application.Dtos.Location;

public record AddLocationDto(string Name);

public record UpdateLocationDto(int Id, string Name);

public record ReadLocationDto(int Id, string Name);