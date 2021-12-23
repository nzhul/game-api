﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.Data;

namespace Server.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AbilityUnitConfiguration", b =>
                {
                    b.Property<string>("AbilitiesCode")
                        .HasColumnType("text");

                    b.Property<int>("UnitConfigurationsId")
                        .HasColumnType("integer");

                    b.HasKey("AbilitiesCode", "UnitConfigurationsId");

                    b.HasIndex("UnitConfigurationsId");

                    b.ToTable("UnitConfigurationAbilities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Server.Models.Games.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("WinnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Server.Models.Items.ItemBlueprint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ItemSlotType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ItemBlueprints");
                });

            modelBuilder.Entity("Server.Models.UnitConfigurations.Ability", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int>("ActionPointsCost")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<int>("Levels")
                        .HasColumnType("integer");

                    b.Property<int>("MovementPointsCost")
                        .HasColumnType("integer");

                    b.Property<int>("ResourceCost")
                        .HasColumnType("integer");

                    b.HasKey("Code");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("Server.Models.UnitConfigurations.UnitConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ActionPoints")
                        .HasColumnType("integer");

                    b.Property<int>("Armor")
                        .HasColumnType("integer");

                    b.Property<int>("ArmorIncrement")
                        .HasColumnType("integer");

                    b.Property<int>("ArmorType")
                        .HasColumnType("integer");

                    b.Property<int>("AttackType")
                        .HasColumnType("integer");

                    b.Property<int>("BuildTime")
                        .HasColumnType("integer");

                    b.Property<int>("Evasion")
                        .HasColumnType("integer");

                    b.Property<int>("Faction")
                        .HasColumnType("integer");

                    b.Property<int>("FoodCost")
                        .HasColumnType("integer");

                    b.Property<int>("GemsCost")
                        .HasColumnType("integer");

                    b.Property<int>("GoldCost")
                        .HasColumnType("integer");

                    b.Property<int>("Hitpoints")
                        .HasColumnType("integer");

                    b.Property<int>("HitpointsIncrement")
                        .HasColumnType("integer");

                    b.Property<int>("Mana")
                        .HasColumnType("integer");

                    b.Property<int>("ManaIncrement")
                        .HasColumnType("integer");

                    b.Property<int>("MaxDamage")
                        .HasColumnType("integer");

                    b.Property<int>("MaxDamageIncrement")
                        .HasColumnType("integer");

                    b.Property<int>("MinDamage")
                        .HasColumnType("integer");

                    b.Property<int>("MinDamageIncrement")
                        .HasColumnType("integer");

                    b.Property<int>("MovementPoints")
                        .HasColumnType("integer");

                    b.Property<int>("OreCost")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("WoodCost")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("UnitConfigurations");
                });

            modelBuilder.Entity("Server.Models.UnitConfigurations.Upgrade", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<int>("GoldCost")
                        .HasColumnType("integer");

                    b.Property<int>("TimeCost")
                        .HasColumnType("integer");

                    b.Property<int>("WoodCost")
                        .HasColumnType("integer");

                    b.HasKey("Code");

                    b.ToTable("Upgrades");
                });

            modelBuilder.Entity("Server.Models.Users.Friendship", b =>
                {
                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<int>("RecieverId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("BecameFriendsTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("RequestTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("SenderId", "RecieverId");

                    b.HasIndex("RecieverId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Server.Models.Users.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateRead")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("MessageSent")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("RecipientDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("RecipientId")
                        .HasColumnType("integer");

                    b.Property<bool>("SenderDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("SenderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Server.Models.Users.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Server.Models.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Server.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<int>("ActiveConnection")
                        .HasColumnType("integer");

                    b.Property<Guid?>("BattleId")
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Discriminator")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int?>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Interests")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MMR")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<byte>("OnlineStatus")
                        .HasColumnType("smallint");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Server.Models.Users.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("UnitConfigurationUpgrade", b =>
                {
                    b.Property<int>("UnitConfigurationsId")
                        .HasColumnType("integer");

                    b.Property<string>("UpgradesCode")
                        .HasColumnType("text");

                    b.HasKey("UnitConfigurationsId", "UpgradesCode");

                    b.HasIndex("UpgradesCode");

                    b.ToTable("UnitConfigurationUpgrades");
                });

            modelBuilder.Entity("AbilityUnitConfiguration", b =>
                {
                    b.HasOne("Server.Models.UnitConfigurations.Ability", null)
                        .WithMany()
                        .HasForeignKey("AbilitiesCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.UnitConfigurations.UnitConfiguration", null)
                        .WithMany()
                        .HasForeignKey("UnitConfigurationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Server.Models.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Server.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Server.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Server.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.Users.Friendship", b =>
                {
                    b.HasOne("Server.Models.Users.User", "Reciever")
                        .WithMany("RecievedFriendRequests")
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Models.Users.User", "Sender")
                        .WithMany("SendFriendRequests")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reciever");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Server.Models.Users.Message", b =>
                {
                    b.HasOne("Server.Models.Users.User", "Recipient")
                        .WithMany("MessagesRecieved")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Server.Models.Users.User", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Server.Models.Users.Photo", b =>
                {
                    b.HasOne("Server.Models.Users.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.Users.UserRole", b =>
                {
                    b.HasOne("Server.Models.Users.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UnitConfigurationUpgrade", b =>
                {
                    b.HasOne("Server.Models.UnitConfigurations.UnitConfiguration", null)
                        .WithMany()
                        .HasForeignKey("UnitConfigurationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.UnitConfigurations.Upgrade", null)
                        .WithMany()
                        .HasForeignKey("UpgradesCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.Users.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Server.Models.Users.User", b =>
                {
                    b.Navigation("MessagesRecieved");

                    b.Navigation("MessagesSent");

                    b.Navigation("Photos");

                    b.Navigation("RecievedFriendRequests");

                    b.Navigation("SendFriendRequests");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
