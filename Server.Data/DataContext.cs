using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Data.Games;
using Server.Data.Items;
using Server.Data.UnitConfigurations;
using Server.Data.Users;
using System;

namespace Server.Data
{
    public class DataContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            // dido: temporary fix for breaking change in postgre database.
            // basically there is new requirement that require for the user to provide timezone.
            // see: https://stackoverflow.com/questions/69961449/net6-and-datetime-problem-cannot-write-datetime-with-kind-utc-to-postgresql-ty
            // probably the problem will be resolved if I use DateTime.Utc everywhere.
            // the question is what happens when you parse JSON and the datetime is automatically created.
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<ItemBlueprint> ItemBlueprints { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<UnitConfiguration> UnitConfigurations { get; set; }

        public DbSet<Ability> Abilities { get; set; }

        public DbSet<Upgrade> Upgrades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Friendship>()
                .HasKey(fs => new { fs.SenderId, fs.RecieverId });

            builder.Entity<Friendship>()
                .HasOne(u => u.Sender)
                .WithMany(fs => fs.SendFriendRequests)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(u => u.SenderId);

            builder.Entity<Friendship>()
                .HasOne(u => u.Reciever)
                .WithMany(fs => fs.RecievedFriendRequests)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(u => u.RecieverId);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(u => u.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(u => u.MessagesRecieved)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UnitConfiguration>()
                .HasIndex(x => x.Type)
                .IsUnique();

            builder.Entity<UnitConfiguration>()
                .HasMany(x => x.Abilities)
                .WithMany(x => x.UnitConfigurations)
                .UsingEntity(x => x.ToTable("UnitConfigurationAbilities"));

            builder.Entity<UnitConfiguration>()
                .HasMany(x => x.Upgrades)
                .WithMany(x => x.UnitConfigurations)
                .UsingEntity(x => x.ToTable("UnitConfigurationUpgrades"));
        }
    }
}
