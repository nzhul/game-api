using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Data.UnitConfigurations;

namespace Server.Data.EntityConfigurations
{
    public class UnitConfigurationConfig : IEntityTypeConfiguration<UnitConfiguration>
    {
        public void Configure(EntityTypeBuilder<UnitConfiguration> builder)
        {
            builder
                .HasIndex(x => x.Type)
                .IsUnique();

            builder
                .HasMany(x => x.Abilities)
                .WithMany(x => x.UnitConfigurations)
                .UsingEntity(x => x.ToTable("UnitConfigurationAbilities"));

            builder
                .HasMany(x => x.Upgrades)
                .WithMany(x => x.UnitConfigurations)
                .UsingEntity(x => x.ToTable("UnitConfigurationUpgrades"));

            // TODO: This can be generic.
            builder
                .Property(x => x.Type)
                .HasConversion<string>();

            builder
                .Property(x => x.PrimaryAttribute)
                .HasConversion<string>();

            builder
                .Property(x => x.Faction)
                .HasConversion<string>();

            builder
                .Property(x => x.AttackType)
                .HasConversion<string>();

            builder
                .Property(x => x.ArmorType)
                .HasConversion<string>();
        }
    }
}
