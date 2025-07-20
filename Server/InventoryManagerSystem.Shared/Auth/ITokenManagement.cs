namespace InventoryManagerSystem.Shared.Auth;

public interface ITokenManagement
{
    Task<(string? Token, DateTime? Expiration, bool Success)> GenerateToken(UserTokenModel userTokenModel);
}