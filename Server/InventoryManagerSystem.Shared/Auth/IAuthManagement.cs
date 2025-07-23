namespace InventoryManagerSystem.Shared.Auth;

public interface IAuthManagement
{
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterRequestDto registerDto);
    Task<(bool IsSuccess, string Message, TokenResponseDto? tokenResponse)> LoginAsync(LoginRequestDto loginDto);
    Task LogoutAsync();
    Task<IEnumerable<UserWithClaimResponseDto>> GetUsersWithClaimsAsync();
    Task SetUpAsync();
    Task<(bool IsSuccess, string Message)> UpdateUserAsync(ChangeUserClaimRequestDto changeUserClaimRequestDto);
    Task<(bool IsSuccess, string Message)> SaveActivityAsync(ActivityTrackerRequestDto activityTrackerRequestDto);
    Task<IEnumerable<ActivityTrackerResponseDto>> GetActivitiesAsync();
    Task SeedRolesAsync();
}