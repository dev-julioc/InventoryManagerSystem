namespace InventoryManagerSystem.Application.Dtos.Category;

public record AddCategoryDto(string Name);

public record UpdateCategoryDto(int Id, string Name);

public record ReadCategoryDto(int Id, string Name);