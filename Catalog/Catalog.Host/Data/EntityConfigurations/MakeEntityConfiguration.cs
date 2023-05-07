using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class MakeEntityConfiguration : IEntityTypeConfiguration<MakeEntity>
{
    public void Configure(EntityTypeBuilder<MakeEntity> builder)
    {
        builder.ToTable("Make");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).UseHiLo("make_hilo").IsRequired();

        builder.Property(i => i.Name).IsRequired();
    }
}