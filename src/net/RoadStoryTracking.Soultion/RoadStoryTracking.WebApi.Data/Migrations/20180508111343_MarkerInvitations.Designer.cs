﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RoadStoryTracking.WebApi.Data.Context;
using RoadStoryTracking.WebApi.Data.Models;
using System;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    [DbContext(typeof(RoadStoryTrackingDbContext))]
    [Migration("20180508111343_MarkerInvitations")]
    partial class MarkerInvitations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role","dbo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim","dbo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim","dbo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin","dbo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole","dbo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken","dbo");
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User","dbo");
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid>("MarkerId");

                    b.Property<DateTimeOffset?>("ModificationDate");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("MarkerId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<string>("RequestedById")
                        .IsRequired();

                    b.Property<string>("RequestedToId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("RequestedToId");

                    b.HasIndex("RequestedById", "RequestedToId")
                        .IsUnique()
                        .HasFilter("[RequestedById] IS NOT NULL AND [RequestedToId] IS NOT NULL");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.Marker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<bool>("IsPrivate");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<DateTimeOffset?>("ModificationDate");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("StartDate");

                    b.Property<int>("Type");

                    b.Property<DateTimeOffset>("ValidOnMapTo");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Markers");
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.MarkerImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<string>("Image");

                    b.Property<Guid>("MarkerId");

                    b.Property<DateTimeOffset?>("ModificationDate");

                    b.HasKey("Id");

                    b.HasIndex("MarkerId");

                    b.ToTable("MarkerImages");
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.MarkerInvitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InvitationStatuses");

                    b.Property<string>("InvitedUserId");

                    b.Property<Guid>("MakerId");

                    b.Property<Guid?>("MarkerId");

                    b.HasKey("Id");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("MarkerId");

                    b.ToTable("MarkerInvitation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.Comment", b =>
                {
                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Comments")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.Marker", "Marker")
                        .WithMany()
                        .HasForeignKey("MarkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.Contact", b =>
                {
                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser", "RequestedBy")
                        .WithMany("Contacts")
                        .HasForeignKey("RequestedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser", "RequestedTo")
                        .WithMany()
                        .HasForeignKey("RequestedToId");
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.Marker", b =>
                {
                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Markers")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.MarkerImage", b =>
                {
                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.Marker", "Marker")
                        .WithMany("Images")
                        .HasForeignKey("MarkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoadStoryTracking.WebApi.Data.Models.MarkerInvitation", b =>
                {
                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.ApplicationUser", "InvitedUser")
                        .WithMany("MarkerInvitations")
                        .HasForeignKey("InvitedUserId");

                    b.HasOne("RoadStoryTracking.WebApi.Data.Models.Marker", "Marker")
                        .WithMany("MarkerInvitations")
                        .HasForeignKey("MarkerId");
                });
#pragma warning restore 612, 618
        }
    }
}
