using InventoryManagerSystem.Application.Services.Interfaces;
using InventoryManagerSystem.Application.Services.Result;
using InventoryManagerSystem.Shared.Auth;

namespace InventoryManagerSystem.Application.Services;

public class AuthService(IAuthManagement authManagement) : IAuthService
{
    public async Task<ResultService> RegisterAsync(RegisterRequestDto registerDto)
    {
        var result = await authManagement.RegisterAsync(registerDto);
        
        return !result.IsSuccess ? 
            ResultService.Fail(result.Message) 
            : ResultService.Ok(result.Message);
    }

    public async Task<ResultService> LoginAsync(LoginRequestDto loginDto)
    {
        var result = await authManagement.LoginAsync(loginDto);
        
        return !result.IsSuccess 
            ? ResultService.Fail(result.Message) 
            : ResultService.Ok(result.tokenResponse);
    }

    public async Task LogoutAsync()
        => await authManagement.LogoutAsync();

    public async Task<IEnumerable<UserWithClaimResponseDto>> GetAllUsersWithClaimsAsync()
        => await authManagement.GetUsersWithClaimsAsync();

    public async Task SetUpAsync()
        => await authManagement.SetUpAsync();

    public async Task<ResultService> UpdateUserAsync(ChangeUserClaimRequestDto changeUserClaimRequestDto)
    {
        var result = await authManagement.UpdateUserAsync(changeUserClaimRequestDto);
        
        return !result.IsSuccess ? 
            ResultService.Fail(result.Message) 
            : ResultService.Ok(result.Message);
    }

    public async Task SaveActivityAsync(ActivityTrackerRequestDto activityTrackerRequestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDto>>> GroupActivitiesAsync()
    {
        throw new NotImplementedException();
    }
}