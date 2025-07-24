using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using InventoryManagerSystem.WEB;
using InventoryManagerSystem.WEB.Providers;
using InventoryManagerSystem.WEB.Services;
using InventoryManagerSystem.WEB.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore(opt =>
{
    opt.AddPolicy("AdministrationPolicy", adp =>
    {
        adp.RequireAuthenticatedUser();
        adp.RequireRole("Admin", "Manager");
    });
    opt.AddPolicy("UserPolicy", adp =>
    {
        adp.RequireAuthenticatedUser();
        adp.RequireRole("User");
    });
});
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddBlazoredLocalStorage();

var apiBaseAddress = "http://localhost:5104";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });

builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();