﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.OrderAPI.Models;

namespace OnlineShop.OrderAPI.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20191128173258_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineShop.Common.Models.OrderAPI.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifyAt");

                    b.Property<int>("ObjectStatus");

                    b.Property<int>("ToTal");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Common.Models.OrderAPI.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifyAt");

                    b.Property<int>("ObjectStatus");

                    b.Property<int?>("OrderId");

                    b.Property<int>("ProductCount");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("OnlineShop.Common.Models.OrderAPI.OrderDetails", b =>
                {
                    b.HasOne("OnlineShop.Common.Models.OrderAPI.Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
