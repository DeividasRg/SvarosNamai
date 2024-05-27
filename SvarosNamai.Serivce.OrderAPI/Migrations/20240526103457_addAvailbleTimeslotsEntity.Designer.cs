﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SvarosNamai.Service.OrderAPI.Data;

#nullable disable

namespace SvarosNamai.Serivce.OrderAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240526103457_addAvailbleTimeslotsEntity")]
    partial class addAvailbleTimeslotsEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SvarosNamai.Serivce.OrderAPI.Models.AvailableTimeSlots", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("AvailableSlots")
                        .HasColumnType("int");

                    b.Property<string>("DayDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OpenSlots")
                        .HasColumnType("int");

                    b.Property<int>("OrderCount")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("AvailableTimeSlots");
                });

            modelBuilder.Entity("SvarosNamai.Serivce.OrderAPI.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int?>("ApartmentNo")
                        .HasColumnType("int");

                    b.Property<string>("BundleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCompany")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<double>("SquareMeters")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SvarosNamai.Serivce.OrderAPI.Models.OrderLine", b =>
                {
                    b.Property<int>("OrderLineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderLineId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderLineId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("SvarosNamai.Serivce.OrderAPI.Models.OrderLog", b =>
                {
                    b.Property<int>("OrderLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderLogId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewOrderStatus")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderLogId");

                    b.ToTable("OrderLogs");
                });

            modelBuilder.Entity("SvarosNamai.Serivce.OrderAPI.Models.Reservations", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("ReservationId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("SvarosNamai.Serivce.OrderAPI.Models.Slots", b =>
                {
                    b.Property<string>("Weekday")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OpenSlots")
                        .HasColumnType("int");

                    b.HasKey("Weekday");

                    b.ToTable("Slots");

                    b.HasData(
                        new
                        {
                            Weekday = "Monday",
                            OpenSlots = 0
                        },
                        new
                        {
                            Weekday = "Tuesday",
                            OpenSlots = 0
                        },
                        new
                        {
                            Weekday = "Wednesday",
                            OpenSlots = 0
                        },
                        new
                        {
                            Weekday = "Thursday",
                            OpenSlots = 0
                        },
                        new
                        {
                            Weekday = "Friday",
                            OpenSlots = 0
                        },
                        new
                        {
                            Weekday = "Saturday",
                            OpenSlots = 0
                        },
                        new
                        {
                            Weekday = "Sunday",
                            OpenSlots = 0
                        });
                });

            modelBuilder.Entity("SvarosNamai.Serivce.OrderAPI.Models.Order", b =>
                {
                    b.HasOne("SvarosNamai.Serivce.OrderAPI.Models.Reservations", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("SvarosNamai.Serivce.OrderAPI.Models.OrderLine", b =>
                {
                    b.HasOne("SvarosNamai.Serivce.OrderAPI.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
