using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Data.Games;
using Server.Data.Items;
using Server.Data.Models.Releases;
using Server.Data.UnitConfigurations;
using Server.Data.Users;
using System;
using System.Reflection;

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

        // Infrastructure
        public DbSet<Release> Releases { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
