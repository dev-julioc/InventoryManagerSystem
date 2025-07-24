using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryManagerSystem.Infra.Data.Auth.Identity;
using InventoryManagerSystem.Shared.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace InventoryManagerSystem.Infra.Data.Auth;

public class TokenManagement(UserManager<ApplicationUser> userManager, IConfiguration configuration) : ITokenManagement
{
    public async Task<(string? Token, DateTime? Expiration, bool Success)> GenerateToken(UserTokenModel userTokenModel)
    {
        var user = await userManager.FindByIdAsync(userTokenModel.Id);

        if (user == null)
            return (null, null, false);
        
        var userClaims = await userManager.GetClaimsAsync(user);
        
        var userRole = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        
        // var claims = new List<Claim>
        // {
        //     new(ClaimTypes.NameIdentifier, user.Id),
        //     new (ClaimTypes.Name, user.Name!),
        //     new (ClaimTypes.Role, userRole)
        // };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.Now.AddDays(configuration.GetSection("JWT").GetValue<double>("ExpirationToken"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userClaims),
            Expires = expiration,
            SigningCredentials = credentials,
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"]
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return (new JwtSecurityTokenHandler().WriteToken(token), expiration, true);
    }
}