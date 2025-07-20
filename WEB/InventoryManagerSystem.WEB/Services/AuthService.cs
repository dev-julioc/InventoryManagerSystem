using System.Net.Http.Json;
using System.Text.Json.Serialization;
using InventoryManagerSystem.WEB.Models.Auth;
using InventoryManagerSystem.WEB.Services.Interfaces;
using InventoryManagerSystem.WEB.Services.Results;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace InventoryManagerSystem.WEB.Services;

public class AuthService(HttpClient httpClient, NavigationManager navigationManager) : IAuthService
{
    private string _basePath = "api/auth";
    
    public async Task<ResultService> RegisterAsync(RegisterRequestDto registerDto)
    {
        var response = await httpClient.PostAsJsonAsync($"{_basePath}/register", registerDto);
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        if(!response.IsSuccessStatusCode)
            return Handlers.ErrorResponse(response.StatusCode, jsonResponse);
        
        var result = JsonConvert.DeserializeObject<ResultService>(jsonResponse);

        return result;
    }

    public async Task<ResultService<TokenResponseDto>> LoginAsync(LoginRequestDto loginDto)
    {
        var response = await httpClient.PostAsJsonAsync($"{_basePath}/login", loginDto);
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        if(!response.IsSuccessStatusCode)
            return Handlers.ErrorResponse<TokenResponseDto>(response.StatusCode, jsonResponse);
        
        var result = JsonConvert.DeserializeObject<ResultService<TokenResponseDto>>(jsonResponse);

        return result;
    }

    public async Task LogoutAsync()
    {
        var response = await httpClient.PostAsync("/logout", null);

        if (!response.IsSuccessStatusCode)
        {
            
        }
        
        navigationManager.NavigateTo("/login");
    }

    public async Task<ResultService<IEnumerable<UserWithClaimResponseDto>>> GetAllUsersWithClaimsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetUpAsync()
    {
        var response = await httpClient.PostAsync($"{_basePath}/set-up", null);

        return response.IsSuccessStatusCode;
    }

    public async Task<ResultService> UpdateUserAsync(ChangeUserClaimRequestDto changeUserClaimRequestDto)
    {
        throw new NotImplementedException();
    }

    public async Task SaveActivityAsync(ActivityTrackerRequestDto activityTrackerRequestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultService<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDto>>>> GroupActivitiesAsync()
    {
        throw new NotImplementedException();
    }
}