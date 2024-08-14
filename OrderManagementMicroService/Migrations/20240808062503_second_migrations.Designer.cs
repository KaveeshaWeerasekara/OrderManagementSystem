﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderProductService;

#nullable disable

namespace OrderProductService.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240808062503_second_migrations")]
    partial class second_migrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OrderProductService.Models.OrderProductModel", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("order_product_id");

                    b.Property<long>("OrderID")
                        .HasColumnType("bigint")
                        .HasColumnName("order_id");

                    b.Property<long>("ProductID")
                        .HasColumnType("bigint")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("id");

                    b.ToTable("orderProduct");
                });
#pragma warning restore 612, 618
        }
    }
}
