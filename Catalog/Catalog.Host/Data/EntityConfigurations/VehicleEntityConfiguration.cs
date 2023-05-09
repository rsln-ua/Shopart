using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class VehicleEntityConfiguration : IEntityTypeConfiguration<VehicleEntity>
{
    public void Configure(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.ToTable("Vehicle");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .UseHiLo("vehicle_hilo")
            .IsRequired();

        builder.Property(i => i.Vin)
            .HasMaxLength(17)
            .IsRequired();

        builder.Property(i => i.Year).IsRequired();

        builder.Property(i => i.Cylinders).IsRequired();

        builder.Property(i => i.EngineSizeL).IsRequired();

        builder.Property(i => i.Mileage).IsRequired();

        builder.Property(i => i.Price).IsRequired();

        builder.HasOne(i => i.Make).WithMany().HasForeignKey(i => i.MakeId);

        builder.HasOne(i => i.Model).WithMany().HasForeignKey(i => i.ModelId);
    }
}