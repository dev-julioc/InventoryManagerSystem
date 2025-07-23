using InventoryManagerSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagerSystem.Infra.Data.EntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");
        builder.HasKey(c => c.Id);

        builder.Property(x => x.Id)
            .HasColumnName("pdt_id")
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .HasColumnName("pdt_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.Property(x => x.SerialNumber)
            .HasColumnName("pdt_serial_number")
            .HasMaxLength(250)
            .IsRequired();
        
        builder.Property(x => x.Price)
            .HasColumnName("pdt_price")
            .HasPrecision(10, 2)
            .IsRequired();
        
        builder.Property(x => x.Quantity)
            .HasColumnName("pdt_quantity")
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("pdt_description")
            .HasMaxLength(250)
            .IsRequired();
        
        builder.Property(x => x.Base64Image)
            .HasColumnName("pdt_base64_image")
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.DateAdded)
            .HasColumnName("pdt_date_added")
            .HasColumnType("timestamp without time zone")
            .IsRequired();
        
        builder.Property(x => x.CategoryId)
            .HasColumnName("ctg_id")
            .IsRequired();
        
        builder.Property(x => x.LocationId)
            .HasColumnName("ltn_id")
            .IsRequired();
        
        builder.HasMany(x => x.Orders).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
        builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Location).WithMany(x => x.Products).HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.Restrict);
    }
}