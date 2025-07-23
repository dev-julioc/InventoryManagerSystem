using InventoryManagerSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagerSystem.Infra.Data.EntitiesConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");
        builder.HasKey(c => c.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ctg_id")
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .HasColumnName("ctg_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
  
    }
}