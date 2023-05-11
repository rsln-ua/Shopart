using Orders.Host.Data.Entities;

namespace Orders.Host.Data.EntityConfigurations;

public class ItemInfoEntityConfiguration : IEntityTypeConfiguration<ItemInfoEntity>
{
    public void Configure(EntityTypeBuilder<ItemInfoEntity> builder)
    {
        builder.ToTable("ItemInfo");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .UseHiLo()
            .IsRequired();

        builder.Property(i => i.OrderId)
            .IsRequired();

        builder.Property(i => i.Name)
            .IsRequired();

        builder.Property(i => i.Price).IsRequired();

        builder.Property(i => i.Quantity).IsRequired();
    }
}