﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicRising.Data;

#nullable disable

namespace MusicRising.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MusicRising.Models.Band", b =>
                {
                    b.Property<string>("BandId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("BandName")
                        .HasColumnType("longtext");

                    b.Property<string>("BandPicture")
                        .HasColumnType("longtext");

                    b.Property<string>("BankAccount")
                        .HasColumnType("longtext");

                    b.Property<int?>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("IdentityUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("Location")
                        .HasColumnType("int");

                    b.HasKey("BandId");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("Bands");
                });

            modelBuilder.Entity("MusicRising.Models.PromoItem", b =>
                {
                    b.Property<string>("PromoItemId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BandId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("IdentityUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Link")
                        .HasColumnType("longtext");

                    b.Property<string>("VenueId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("PromoItemId");

                    b.HasIndex("BandId");

                    b.HasIndex("IdentityUserId");

                    b.HasIndex("VenueId");

                    b.ToTable("PromoItems");
                });

            modelBuilder.Entity("MusicRising.Models.Rating", b =>
                {
                    b.Property<string>("RatingId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BandId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IdentityUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<string>("VenueId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("RatingId");

                    b.HasIndex("BandId");

                    b.HasIndex("IdentityUserId");

                    b.HasIndex("VenueId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("MusicRising.Models.Show", b =>
                {
                    b.Property<string>("ShowId")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("BandFee")
                        .HasColumnType("double");

                    b.Property<string>("BandId")
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<bool>("Payed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PromoLink")
                        .HasColumnType("longtext");

                    b.Property<double?>("ShowFee")
                        .HasColumnType("double");

                    b.Property<string>("VenueId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("ShowId");

                    b.HasIndex("BandId");

                    b.HasIndex("VenueId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("MusicRising.Models.Venue", b =>
                {
                    b.Property<string>("VenueId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BankAccount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("IdentityUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("VenueName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VenuePicture")
                        .HasColumnType("longtext");

                    b.HasKey("VenueId");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicRising.Models.Band", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("IdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MusicRising.Models.PromoItem", b =>
                {
                    b.HasOne("MusicRising.Models.Band", "Band")
                        .WithMany("PromoItems")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("IdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicRising.Models.Venue", "Venue")
                        .WithMany("PromoItems")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Band");

                    b.Navigation("User");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("MusicRising.Models.Rating", b =>
                {
                    b.HasOne("MusicRising.Models.Band", "Band")
                        .WithMany("Ratings")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("IdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicRising.Models.Venue", "Venue")
                        .WithMany("Ratings")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Band");

                    b.Navigation("User");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("MusicRising.Models.Show", b =>
                {
                    b.HasOne("MusicRising.Models.Band", "HeadLiner")
                        .WithMany("Shows")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("MusicRising.Models.Venue", "Venue")
                        .WithMany("Shows")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HeadLiner");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("MusicRising.Models.Venue", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("IdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MusicRising.Models.Band", b =>
                {
                    b.Navigation("PromoItems");

                    b.Navigation("Ratings");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("MusicRising.Models.Venue", b =>
                {
                    b.Navigation("PromoItems");

                    b.Navigation("Ratings");

                    b.Navigation("Shows");
                });
#pragma warning restore 612, 618
        }
    }
}
