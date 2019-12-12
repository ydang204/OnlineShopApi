﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.UserAPI.Models;

namespace OnlineShop.UserAPI.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20191201151018_InitUserDatabase")]
    partial class InitUserDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineShop.Common.Models.UserAPI.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifyAt");

                    b.Property<int>("ObjectStatus");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("OnlineShop.Common.Models.UserAPI.AccountRole", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<int>("RoleId");

                    b.HasKey("AccountId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AccountRoles");
                });

            modelBuilder.Entity("OnlineShop.Common.Models.UserAPI.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifyAt");

                    b.Property<string>("Name");

                    b.Property<int>("ObjectStatus");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("OnlineShop.Common.Models.UserAPI.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifyAt");

                    b.Property<string>("Name");

                    b.Property<int>("ObjectStatus");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OnlineShop.Common.Models.UserAPI.RolePermission", b =>
                {
                    b.Property<int>("PermissionId");

                    b.Property<int>("RoleId");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("TrackerEnabledDbContext.Common.Models.AuditLog", b =>
                {
                    b.Property<long>("AuditLogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EventDateUTC");

                    b.Property<int>("EventType");

                    b.Property<string>("RecordId")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("TypeFullName")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<string>("UserName");

                    b.HasKey("AuditLogId");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("TrackerEnabledDbContext.Common.Models.AuditLogDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuditLogId");

                    b.Property<string>("NewValue");

                    b.Property<string>("OriginalValue");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AuditLogId");

                    b.ToTable("AuditLogDetails");
                });

            modelBuilder.Entity("TrackerEnabledDbContext.Common.Models.LogMetadata", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuditLogId");

                    b.Property<string>("Key");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("AuditLogId");

                    b.ToTable("LogMetadata");
                });

            modelBuilder.Entity("OnlineShop.Common.Models.UserAPI.AccountRole", b =>
                {
                    b.HasOne("OnlineShop.Common.Models.UserAPI.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineShop.Common.Models.UserAPI.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineShop.Common.Models.UserAPI.RolePermission", b =>
                {
                    b.HasOne("OnlineShop.Common.Models.UserAPI.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineShop.Common.Models.UserAPI.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackerEnabledDbContext.Common.Models.AuditLogDetail", b =>
                {
                    b.HasOne("TrackerEnabledDbContext.Common.Models.AuditLog", "Log")
                        .WithMany("LogDetails")
                        .HasForeignKey("AuditLogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackerEnabledDbContext.Common.Models.LogMetadata", b =>
                {
                    b.HasOne("TrackerEnabledDbContext.Common.Models.AuditLog", "AuditLog")
                        .WithMany("Metadata")
                        .HasForeignKey("AuditLogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
