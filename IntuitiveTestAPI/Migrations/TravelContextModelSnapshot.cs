﻿// <auto-generated />
using IntuitiveTestAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntuitiveTestAPI.Migrations
{
    [DbContext(typeof(TravelContext))]
    partial class TravelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IntuitiveTestAPI.Airport", b =>
                {
                    b.Property<int>("AirportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirportID"));

                    b.Property<int>("GeographyLevel1ID")
                        .HasColumnType("int");

                    b.Property<string>("IATACode")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("AirportID");

                    b.HasIndex("GeographyLevel1ID");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("IntuitiveTestAPI.AirportGroup", b =>
                {
                    b.Property<int>("AirportGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirportGroupID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("AirportGroupID");

                    b.ToTable("AirportGroups");
                });

            modelBuilder.Entity("IntuitiveTestAPI.AirportInAirportGroup", b =>
                {
                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.Property<int>("AirportGroupID")
                        .HasColumnType("int");

                    b.HasKey("AirportID", "AirportGroupID");

                    b.HasIndex("AirportGroupID");

                    b.ToTable("AirportInAirportGroups");
                });

            modelBuilder.Entity("IntuitiveTestAPI.Country", b =>
                {
                    b.Property<int>("GeographyLevel1ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GeographyLevel1ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("GeographyLevel1ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("IntuitiveTestAPI.Route", b =>
                {
                    b.Property<int>("RouteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteID"));

                    b.Property<int>("ArrivalAirportID")
                        .HasColumnType("int");

                    b.Property<int>("DepartureAirportID")
                        .HasColumnType("int");

                    b.HasKey("RouteID");

                    b.HasIndex("ArrivalAirportID");

                    b.HasIndex("DepartureAirportID");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("IntuitiveTestAPI.RouteWithGroup", b =>
                {
                    b.Property<int>("RouteWithGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteWithGroupID"));

                    b.Property<int>("ArrivalAirportGroupID")
                        .HasColumnType("int");

                    b.Property<int>("DepartureAirportGroupID")
                        .HasColumnType("int");

                    b.HasKey("RouteWithGroupID");

                    b.HasIndex("ArrivalAirportGroupID");

                    b.HasIndex("DepartureAirportGroupID");

                    b.ToTable("RouteWithGroup");
                });

            modelBuilder.Entity("IntuitiveTestAPI.Airport", b =>
                {
                    b.HasOne("IntuitiveTestAPI.Country", "Country")
                        .WithMany("Airports")
                        .HasForeignKey("GeographyLevel1ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("IntuitiveTestAPI.AirportInAirportGroup", b =>
                {
                    b.HasOne("IntuitiveTestAPI.AirportGroup", "AirportGroup")
                        .WithMany("AirportInAirportGroups")
                        .HasForeignKey("AirportGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntuitiveTestAPI.Airport", "Airport")
                        .WithMany("AirportInAirportGroups")
                        .HasForeignKey("AirportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airport");

                    b.Navigation("AirportGroup");
                });

            modelBuilder.Entity("IntuitiveTestAPI.Route", b =>
                {
                    b.HasOne("IntuitiveTestAPI.Airport", "ArrivalAirport")
                        .WithMany()
                        .HasForeignKey("ArrivalAirportID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IntuitiveTestAPI.Airport", "DepartureAirport")
                        .WithMany()
                        .HasForeignKey("DepartureAirportID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ArrivalAirport");

                    b.Navigation("DepartureAirport");
                });

            modelBuilder.Entity("IntuitiveTestAPI.RouteWithGroup", b =>
                {
                    b.HasOne("IntuitiveTestAPI.AirportGroup", "ArrivalAirportGroup")
                        .WithMany()
                        .HasForeignKey("ArrivalAirportGroupID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IntuitiveTestAPI.AirportGroup", "DepartureAirportGroup")
                        .WithMany()
                        .HasForeignKey("DepartureAirportGroupID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ArrivalAirportGroup");

                    b.Navigation("DepartureAirportGroup");
                });

            modelBuilder.Entity("IntuitiveTestAPI.Airport", b =>
                {
                    b.Navigation("AirportInAirportGroups");
                });

            modelBuilder.Entity("IntuitiveTestAPI.AirportGroup", b =>
                {
                    b.Navigation("AirportInAirportGroups");
                });

            modelBuilder.Entity("IntuitiveTestAPI.Country", b =>
                {
                    b.Navigation("Airports");
                });
#pragma warning restore 612, 618
        }
    }
}
