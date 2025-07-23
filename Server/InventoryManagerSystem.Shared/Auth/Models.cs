namespace InventoryManagerSystem.Shared.Auth;

public record UserTokenModel(string Id, string Name, string Email);

public record UserResponseDto
(
    string Id,
    string Name,
    string UserName,
    string Email
);

public record TokenResponseDto
(
    string AccessToken,
    DateTime ExpiresIn
);
public record LoginRequestDto(string Email, string Password);

public record RegisterRequestDto(string Name, string Email, string Password, string ConfirmPassword, string Policy);

public record UserWithClaimResponseDto(string Email, string UserId, string Name, string RoleName, bool ManagerUser, bool Read, bool Delete, bool Update, bool Create);

public record ChangeUserClaimRequestDto(string UserId, string Name, string RoleName, bool ManagerUser, bool Read, bool Delete, bool Update, bool Create);

public record ActivityTrackerRequestDto(DateTime Date, string Title, string Description, bool OperationState, string UserId);

public record ActivityTrackerResponseDto(string UserName, DateTime Date, string Title, string Description, bool OperationState, string UserId);

public static class Policy
{
    public const string AdminPolicy = "AdminPolicy";
    public const string ManagerPolicy = "ManagerPolicy";
    public const string UserPolicy = "UserPolicy";

    public static class RoleClaim
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }
}