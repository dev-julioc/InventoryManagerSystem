using InventoryManagerSystem.Application.Services.Result;
using InventoryManagerSystem.Shared.Auth;

namespace InventoryManagerSystem.Application.Services.Interfaces;

public interface IAuthService
{
    Task<ResultService> RegisterAsync(RegisterRequestDto registerDto);
    Task<ResultService> LoginAsync(LoginRequestDto loginDto);
    Task LogoutAsync();
    Task<IEnumerable<UserWithClaimResponseDto>> GetAllUsersWithClaimsAsync();
    Task SetUpAsync();
    Task<ResultService> UpdateUserAsync(ChangeUserClaimRequestDto changeUserClaimRequestDto);
    Task SaveActivityAsync(ActivityTrackerRequestDto activityTrackerRequestDto);
    Task<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDto>>> GroupActivitiesAsync();
}