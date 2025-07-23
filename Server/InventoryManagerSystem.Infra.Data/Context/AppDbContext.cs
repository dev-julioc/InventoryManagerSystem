using InventoryManagerSystem.Domain.Entities;
using InventoryManagerSystem.Infra.Data.Auth.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerSystem.Infra.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Tracker> ActivityTracker { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}