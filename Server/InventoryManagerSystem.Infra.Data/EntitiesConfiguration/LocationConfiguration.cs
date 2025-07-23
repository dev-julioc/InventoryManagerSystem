using InventoryManagerSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagerSystem.Infra.Data.EntitiesConfiguration;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("location");
        builder.HasKey(c => c.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ltn_id")
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .HasColumnName("ltn_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.HasMany(x => x.Products).WithOne(x => x.Location).HasForeignKey(x => x.LocationId);
    }
}