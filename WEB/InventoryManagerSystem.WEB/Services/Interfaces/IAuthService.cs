using System.Security.Claims;
using InventoryManagerSystem.WEB.Models.Auth;
using InventoryManagerSystem.WEB.Services.Results;

namespace InventoryManagerSystem.WEB.Services.Interfaces;

public interface IAuthService
{
    Task<ResultService> RegisterAsync(RegisterRequestDto registerDto);
    Task<ResultService<TokenResponseDto>> LoginAsync(LoginRequestDto loginDto);
    Task<ResultService> LogoutAsync();
    Task<ResultService<IEnumerable<UserWithClaimResponseDto>>> GetAllUsersWithClaimsAsync();
    Task<bool> SetUpAsync();
    Task<ResultService> UpdateUserAsync(ChangeUserClaimRequestDto changeUserClaimRequestDto);
    Task SaveActivityAsync(ActivityTrackerRequestDto activityTrackerRequestDto);
    Task<ResultService<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDto>>>> GroupActivitiesAsync();
    
    bool CustomClaimChecker(ClaimsPrincipal user, string specificClaim);
}