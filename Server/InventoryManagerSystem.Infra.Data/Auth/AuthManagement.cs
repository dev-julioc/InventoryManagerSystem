using System.Security.Claims;
using InventoryManagerSystem.Infra.Data.Auth.Identity;
using InventoryManagerSystem.Shared.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InventoryManagerSystem.Infra.Data.Auth;

public class AuthManagement(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenManagement tokenManagement, 
    RoleManager<IdentityRole> roleManager, IConfiguration configuration) : IAuthManagement
{
    public async Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterRequestDto registerDto)
    {
        var user = await FindUserByEmailAsync(registerDto.Email);

        if (user is not null)
            return (false, "User already exists.");

        var newUser = new ApplicationUser
        {
            UserName = registerDto.Email,
            PasswordHash = registerDto.Password,
            Email = registerDto.Email,
            Name = registerDto.Name,
        };
        
        var result = await userManager.CreateAsync(newUser, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(_ => _.Description);
            return (false, string.Join('\n', errors));
        }

        return await CreateUserClaims(registerDto);
    }

    public async Task<(bool IsSuccess, string Message, TokenResponseDto? tokenResponse)> LoginAsync(LoginRequestDto loginDto)
    {
        var user = await FindUserByEmailAsync(loginDto.Email);
        
        if(user is null)
            return (false, "User does not found.", null);
        
        var verifyPassword = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!verifyPassword.Succeeded)
            return (false, "Invalid credentials.", null);
        
        var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

        if (!result.Succeeded)
            return (false, "Unknown error occured while logging you in.", null);
        
        var userToken = new UserTokenModel(user.Id, user.UserName!, user.Email!);
        
        var responseToken = await tokenManagement.GenerateToken(userToken);

        if (!responseToken.Success)
            return (false, "Error to generate the token.", null);
        
        var tokenResponse = new TokenResponseDto
        (
            responseToken.Token!,
            responseToken.Expiration!.Value
        );
        
        return (true, "Login successfully", tokenResponse);
    }

    public async Task LogoutAsync()
        => await signInManager.SignOutAsync();
    
    public async Task<IEnumerable<UserWithClaimResponseDto>> GetUsersWithClaimsAsync()
    {
        var userList = new List<UserWithClaimResponseDto>();
        var allUsers = await userManager.Users.ToListAsync();

        foreach (var user in allUsers)
        {
            var currentUser = await userManager.FindByIdAsync(user.Id);
            
            var getCurrentUserClaim = await userManager.GetClaimsAsync(currentUser);
            
            if(getCurrentUserClaim.Any())
                userList.Add(
                    new UserWithClaimResponseDto
                    (
                        getCurrentUserClaim.FirstOrDefault(_ => _.Type == ClaimTypes.Email).Value,
                        user.Id,
                        getCurrentUserClaim.FirstOrDefault(_ => _.Type == "Name").Value,
                        getCurrentUserClaim.FirstOrDefault(_ => _.Type == ClaimTypes.Role).Value,
                        Convert.ToBoolean(getCurrentUserClaim.FirstOrDefault(_ => _.Type == "ManagerUser").Value) ,
                        Convert.ToBoolean(getCurrentUserClaim.FirstOrDefault(_ => _.Type == "Read").Value) ,
                        Convert.ToBoolean(getCurrentUserClaim.FirstOrDefault(_ => _.Type == "Delete").Value),
                        Convert.ToBoolean(getCurrentUserClaim.FirstOrDefault(_ => _.Type == "Update").Value),
                        Convert.ToBoolean(getCurrentUserClaim.FirstOrDefault(_ => _.Type == "Create").Value)
                    ));
        }
        
        return userList;
    }

    public async Task SetUpAsync()
        => await RegisterAsync(
                new RegisterRequestDto
                (
                    "Administrator",
                    "admin@admin.com",
                    "Admin@123",
                    "Admin@123",
                    Policy.AdminPolicy
                ));

    public async Task<(bool IsSuccess, string Message)> UpdateUserAsync(ChangeUserClaimRequestDto changeUserClaimRequestDto)
    {
        var user = await FindUserByIdAsync(changeUserClaimRequestDto.UserId);
        if (user is null)
            return (false, "User not found.");
        
        var oldUserClaims = await userManager.GetClaimsAsync(user);

        Claim[] newUserClaims =
        [
            new (ClaimTypes.Email, user.Email!),
            new (ClaimTypes.Role, changeUserClaimRequestDto.RoleName),
            new ("Name", changeUserClaimRequestDto.Name),
            new ("Create", changeUserClaimRequestDto.Create.ToString()),
            new ("Update", changeUserClaimRequestDto.Update.ToString()),
            new ("Delete", changeUserClaimRequestDto.Delete.ToString()),
            new ("Read", changeUserClaimRequestDto.Read.ToString()),
            new ("ManagerUser", changeUserClaimRequestDto.ManagerUser.ToString())
        ];
        
        var result = await userManager.RemoveClaimsAsync(user, oldUserClaims);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(_ => _.Description);
            return (false, string.Join('\n', errors));
        }
        
        var addNewClaims = await userManager.AddClaimsAsync(user, newUserClaims);
        
        if (!addNewClaims.Succeeded)
        {
            var errors = addNewClaims.Errors.Select(_ => _.Description);
            return (false, string.Join('\n', errors));
        }

        return (true, "User updated.");
    }

    public async Task<(bool IsSuccess, string Message)> SaveActivityAsync(ActivityTrackerRequestDto activityTrackerRequestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ActivityTrackerResponseDto>> GetActivitiesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task SeedRolesAsync()
    {
        var roles = new[] { "Admin", "User", "Manager" };

        foreach (var roleName in roles)
        {
            if (await roleManager.RoleExistsAsync(roleName)) continue;
            
            var role = new IdentityRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };

            var roleResult = await roleManager.CreateAsync(role);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new Exception($"Error creating roles: {errors}");
            }
        }
    }

    private async Task<(bool IsSuccess, string Message)> CreateUserClaims(RegisterRequestDto registerDto)
    {
        if (string.IsNullOrEmpty(registerDto.Policy))
            return (false, "No policy specified.");

        Claim[] userClaims = [];

        if (registerDto.Policy.Equals(Policy.AdminPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userClaims =
            [
                new Claim(ClaimTypes.Email, registerDto.Email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("Name", registerDto.Name),
                new Claim("Create", "true"),
                new Claim("Update", "true"),
                new Claim("Delete", "true"),
                new Claim("Read", "true"),
                new Claim("ManagerUser", "true")
            ];
        }
        else if (registerDto.Policy.Equals(Policy.ManagerPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userClaims =
            [
                new Claim(ClaimTypes.Email, registerDto.Email),
                new Claim(ClaimTypes.Role, "Manager"),
                new Claim("Name", registerDto.Name),
                new Claim("Create", "true"),
                new Claim("Update", "true"),
                new Claim("Delete", "false"),
                new Claim("Read", "true"),
                new Claim("ManagerUser", "false")
            ];
        }
        else if (registerDto.Policy.Equals(Policy.UserPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userClaims =
            [
                new Claim(ClaimTypes.Email, registerDto.Email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("Name", registerDto.Name),
                new Claim("Create", "false"),
                new Claim("Update", "false"),
                new Claim("Delete", "false"),
                new Claim("Read", "false"),
                new Claim("ManagerUser", "false")
            ];
        }

        var result = await userManager.AddClaimsAsync((await FindUserByEmailAsync(registerDto.Email))!, userClaims);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(_ => _.Description);
            return (false, string.Join('\n', errors));
        }

        return (true, "User created.");
    }

    private async Task<ApplicationUser?> FindUserByEmailAsync(string email)
        => await userManager.FindByEmailAsync(email);

    private async Task<ApplicationUser?> FindUserByIdAsync(string id)
        => await userManager.FindByIdAsync(id);
}