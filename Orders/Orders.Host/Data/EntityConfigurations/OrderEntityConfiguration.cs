using Orders.Host.Data.Entities;

namespace Orders.Host.Data.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).UseHiLo().IsRequired();

        builder.Property(i => i.Total).IsRequired();

        builder.Property(i => i.UserId).IsRequired();

        builder.Property(i => i.CreatedAt).IsRequired();

        builder.HasMany(i => i.Items).WithOne().HasForeignKey(i => i.OrderId).IsRequired();
    }
}