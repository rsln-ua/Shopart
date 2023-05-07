using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class ModelEntityConfiguration : IEntityTypeConfiguration<ModelEntity>
{
    public void Configure(EntityTypeBuilder<ModelEntity> builder)
    {
        builder.ToTable("Model");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).UseHiLo("model_hilo").IsRequired();

        builder.Property(i => i.Name).IsRequired();

        builder.HasOne(i => i.Make).WithMany().HasForeignKey(i => i.MakeId);
    }
}