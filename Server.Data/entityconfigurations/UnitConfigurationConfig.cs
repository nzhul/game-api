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
        }
    }
}
