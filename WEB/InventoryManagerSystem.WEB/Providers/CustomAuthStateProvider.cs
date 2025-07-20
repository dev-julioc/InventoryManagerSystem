using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;

namespace InventoryManagerSystem.WEB.Providers;

using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetItemAsStringAsync("authToken");

        if (string.IsNullOrWhiteSpace(token) || IsExpiredToken(token))
        {
            await localStorageService.RemoveItemAsync("authToken");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
            

        var handler = new JwtSecurityTokenHandler();
        
        var jwt = handler.ReadJwtToken(token);
        
        var identity = new ClaimsIdentity(jwt.Claims, "jwt");

        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }
    
    private static bool IsExpiredToken(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        
        var token = handler.ReadJwtToken(jwtToken);
        
        var expoClaim = token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

        if (expoClaim == null)
            return true;

        var exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expoClaim));
        
        return exp < DateTimeOffset.UtcNow;
    }

    public void NotifyUserAuthentication(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        var jwt = handler.ReadJwtToken(token);
        
        var identity = new ClaimsIdentity(jwt.Claims, "jwt");

        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}