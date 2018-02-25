﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ParkingApp.Data;
using System;

namespace ParkingApp.Data.Migrations
{
    [DbContext(typeof(ParkingContext))]
    [Migration("20180225031802_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ParkingApp.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ParkingApp.Domain.Parking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckIn");

                    b.Property<DateTime?>("CheckOut");

                    b.Property<string>("Ticket")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Parking");
                });

            modelBuilder.Entity("ParkingApp.Domain.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CateogoryId");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("CateogoryId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("ParkingApp.Domain.Parking", b =>
                {
                    b.HasOne("ParkingApp.Domain.Vehicle", "Vehicle")
                        .WithMany("Parkings")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParkingApp.Domain.Vehicle", b =>
                {
                    b.HasOne("ParkingApp.Domain.Category", "Category")
                        .WithMany("Vehicles")
                        .HasForeignKey("CateogoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}