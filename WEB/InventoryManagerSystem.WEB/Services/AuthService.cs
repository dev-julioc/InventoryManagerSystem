using System.Net.Http.Json;
using InventoryManagerSystem.WEB.Models.Auth;
using InventoryManagerSystem.WEB.Services.Interfaces;
using InventoryManagerSystem.WEB.Services.Results;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using InventoryManagerSystem.WEB.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;




namespace InventoryManagerSystem.WEB.Services;

public class AuthService(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorageService, AuthenticationStateProvider authStateProvider) : IAuthService
{
    private string _basePath = "api/auth";
    
    public async Task<ResultService> RegisterAsync(RegisterRequestDto registerDto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync($"{_basePath}/register", registerDto);
        
            var jsonResponse = await response.Content.ReadAsStringAsync();
        
            if(!response.IsSuccessStatusCode)
                return Handlers.ErrorResponse(response.StatusCode, jsonResponse);
        
            var result = JsonConvert.DeserializeObject<ResultService>(jsonResponse);

            return result;
        }
        catch (Exception e)
        {
            return new ResultService { IsSuccess = false, Message = $"Erro ao conectar com o servidor. {e.Message}" };
        }
       
    }

    public async Task<ResultService<TokenResponseDto>> LoginAsync(LoginRequestDto loginDto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync($"{_basePath}/login", loginDto);
        
            var jsonResponse = await response.Content.ReadAsStringAsync();
        
            if(!response.IsSuccessStatusCode)
                return Handlers.ErrorResponse<TokenResponseDto>(response.StatusCode, jsonResponse);
        
            var result = JsonConvert.DeserializeObject<ResultService<TokenResponseDto>>(jsonResponse);

            return result;
        }
        catch (Exception e)
        {
            return new ResultService<TokenResponseDto> { IsSuccess = false, Message = $"Erro ao conectar com o servidor. {e.Message}" };
        }
       
    }

    public async Task<ResultService> LogoutAsync()
    {
        try
        {
            var response = await httpClient.PostAsync($"{_basePath}/logout", null);
        
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if(!response.IsSuccessStatusCode)
                return Handlers.ErrorResponse(response.StatusCode, jsonResponse);
        
            await localStorageService.RemoveItemAsync("authToken");
        
            if (authStateProvider is CustomAuthStateProvider customAuthProvider)
            {
                customAuthProvider.NotifyUserLogout();
            }
        
            //navigationManager.NavigateTo("/login");

            return new ResultService { IsSuccess = true, Message = "Logout successfully" };
        }
        catch (Exception e)
        {
            return new ResultService { IsSuccess = false, Message = $"Erro ao conectar com o servidor. {e.Message}" };
        }
        
    }

    public async Task<ResultService<IEnumerable<UserWithClaimResponseDto>>> GetAllUsersWithClaimsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetUpAsync()
    {
        try
        {
            var response = await httpClient.PostAsync($"{_basePath}/set-up", null);

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            return false;
        }
       
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