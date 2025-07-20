using System.ComponentModel.DataAnnotations;

namespace InventoryManagerSystem.WEB.Models.Auth;

public class LoginRequestDto
{
    [Required]
    [Length(3, 200)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}

public class RegisterRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Policy { get; set; } = string.Empty;
}

public class UserWithClaimResponseDto
{
    public string Email { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool ManagerUser { get; set; }
    public bool Read { get; set; }
    public bool Delete { get; set; }
    public bool Update { get; set; }
    public bool Create { get; set; }
}

public class ChangeUserClaimRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool ManagerUser { get; set; }
    public bool Read { get; set; }
    public bool Delete { get; set; }
    public bool Update { get; set; }
    public bool Create { get; set; }
}

public class ActivityTrackerRequestDto
{
    // Adicione propriedades se necessário
}

public class ActivityTrackerResponseDto
{
    // Adicione propriedades se necessário
}

public record TokenResponseDto
(
    string AccessToken,
    DateTime ExpiresIn
);
