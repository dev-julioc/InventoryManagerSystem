using Microsoft.AspNetCore.Identity;

namespace InventoryManagerSystem.Infra.Data.Auth.Identity;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
}