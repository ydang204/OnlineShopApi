﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.NotificationAPI.Models;

namespace OnlineShop.NotificationAPI.Migrations
{
    [DbContext(typeof(NotificationContext))]
    partial class NotificationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineShop.Common.Models.Notification.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("DeviceUniqueIdentify");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<int>("ObjectStatus");

                    b.Property<int>("Platform");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("OnlineShop.Common.Models.Notification.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("Data");

                    b.Property<int?>("DataId");

                    b.Property<string>("Message");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<int>("ObjectStatus");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
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
