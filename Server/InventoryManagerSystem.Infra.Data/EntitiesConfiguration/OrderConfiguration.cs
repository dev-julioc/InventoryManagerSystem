using InventoryManagerSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagerSystem.Infra.Data.EntitiesConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order");
        builder.HasKey(c => c.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ord_id")
            .UseIdentityColumn();

        builder.Property(x => x.DateOrdered)
            .HasColumnName("ord_date_ordered")
            .HasColumnType("timestamp without time zone")
            .IsRequired();
        
        builder.Property(x => x.DeliveringDate)
            .HasColumnName("ord_delivering_date")
            .HasColumnType("timestamp without time zone");
        
        builder.Property(x => x.Quantity)
            .HasColumnName("ord_quantity")
            .IsRequired();
        
        builder.Property(x => x.Price)
            .HasColumnName("ord_price")
            .HasPrecision(10, 2)
            .IsRequired();
        
        builder.Property(x => x.TotalAmount)
            .HasColumnName("ord_total_amount")
            .HasPrecision(10, 2)
            .IsRequired();
        
        builder.Property(x => x.OrderState)
            .HasColumnName("ord_order_state")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.ProductId)
            .HasColumnName("pdt_id")
            .IsRequired();
        
        builder.Property(x => x.ClientId)
            .HasColumnName("usr_id")
            .IsRequired();
        
        
        builder.HasOne(x => x.Product).WithMany(x => x.Orders).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
    }
}