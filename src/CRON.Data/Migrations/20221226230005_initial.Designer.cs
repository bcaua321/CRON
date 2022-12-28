﻿// <auto-generated />
using System;
using CRON.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRON.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221226230005_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CRON.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("bar_code");

                    b.Property<string>("Brands")
                        .HasColumnType("longtext")
                        .HasColumnName("brands");

                    b.Property<string>("Categories")
                        .HasColumnType("longtext")
                        .HasColumnName("categories");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("code");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext")
                        .HasColumnName("image_url");

                    b.Property<DateTime>("ImportedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("imported_t");

                    b.Property<string>("Packaging")
                        .HasColumnType("longtext")
                        .HasColumnName("packaging");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("product_name");

                    b.Property<string>("Quantity")
                        .HasColumnType("longtext")
                        .HasColumnName("quantity");

                    b.Property<string>("Status")
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.ToTable("products");
                });
#pragma warning restore 612, 618
        }
    }
}
