using InventoryManagerSystem.Infra.Data.Auth.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InventoryManagerSystem.Domain.Entities;

namespace InventoryManagerSystem.Infra.Data.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Tracker> ActivityTracker { get; set; }
}