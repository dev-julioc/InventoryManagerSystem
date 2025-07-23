using EntityFramework.Exceptions.PostgreSQL;
using InventoryManagerSystem.Application.Services;
using InventoryManagerSystem.Application.Services.Interfaces;
using InventoryManagerSystem.Domain.Interfaces;
using InventoryManagerSystem.Infra.Data.Auth;
using InventoryManagerSystem.Infra.Data.Auth.Identity;
using InventoryManagerSystem.Infra.Data.Context;
using InventoryManagerSystem.Infra.Data.Repositories;
using InventoryManagerSystem.Shared.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace InventoryManagerSystem.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyPostgresDb");
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        var searchPaths = connectionStringBuilder.SearchPath?.Split(',');

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, b =>
            {
                b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);

                if (searchPaths is { Length: > 0 })
                {
                    var mainSchema = searchPaths[0];
                    b.MigrationsHistoryTable(HistoryRepository.DefaultTableName, mainSchema);
                }
            }).UseExceptionProcessor();
        });

        // services.AddAuthentication(opt =>
        // {
        //     opt.DefaultScheme = IdentityConstants.ApplicationScheme;
        //     opt.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        // }).AddIdentityCookies();
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddScoped<IAuthManagement, AuthManagement>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenManagement, TokenManagement>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ILocationService, LocationService>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
      

        return services;
    }
    
    public static async Task SeedRoles(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var seed = serviceScope.ServiceProvider.GetService<IAuthManagement>();
    
        if (seed != null)
        {
            await seed.SeedRolesAsync();
        }
    }
    
}