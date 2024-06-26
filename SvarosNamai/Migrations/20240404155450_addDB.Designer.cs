﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SvarosNamai.Service.ProductAPI.Data;

#nullable disable

namespace SvarosNamai.Service.ProductAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240404155450_addDB")]
    partial class addDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SvarosNamai.Service.ProductAPI.Models.Bundle", b =>
                {
                    b.Property<int>("BundleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BundleId"));

                    b.Property<string>("BundleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Discount")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("BundleId");

                    b.ToTable("Bundles");
                });

            modelBuilder.Entity("SvarosNamai.Service.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("BundleId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("BundleId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SvarosNamai.Service.ProductAPI.Models.Product", b =>
                {
                    b.HasOne("SvarosNamai.Service.ProductAPI.Models.Bundle", "Bundle")
                        .WithMany("Products")
                        .HasForeignKey("BundleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bundle");
                });

            modelBuilder.Entity("SvarosNamai.Service.ProductAPI.Models.Bundle", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
