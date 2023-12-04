using Microsoft.EntityFrameworkCore;


namespace IntuitiveTestAPI
{
    public class TravelContext:DbContext
    {
        public TravelContext(DbContextOptions<TravelContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //country to airports- one to many relationship
            modelBuilder.Entity<Country>()
                .HasMany<Airport>(c => c.Airports)
                .WithOne(a => a.Country)
                .HasForeignKey(a=>a.GeographyLevel1ID)
                .IsRequired();

            //one route has one departure airport and one arrival airport
            modelBuilder.Entity<Route>()
                .HasOne<Airport>(r => r.DepartureAirport)
                .WithMany()
                .HasForeignKey(r => r.DepartureAirportID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Route>()
                .HasOne<Airport>(r => r.ArrivalAirport)
                .WithMany()
                .HasForeignKey(r => r.ArrivalAirportID)
                .OnDelete(DeleteBehavior.Restrict);

            //one route has one departure airport grp and one arrival airport grp
            modelBuilder.Entity<RouteWithGroup>()
                .HasOne<AirportGroup>(r => r.DepartureAirportGroup)
                .WithMany()
                .HasForeignKey(r => r.DepartureAirportGroupID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RouteWithGroup>()
                .HasOne<AirportGroup>(r => r.ArrivalAirportGroup)
                .WithMany()
                .HasForeignKey(r => r.ArrivalAirportGroupID)
                .OnDelete(DeleteBehavior.Restrict);

            //airportgrp to airports- many to many relationship
            modelBuilder.Entity<AirportInAirportGroup>()
                .HasKey(k => new { k.AirportID, k.AirportGroupID });

            modelBuilder.Entity<AirportInAirportGroup>()
                .HasOne<Airport>(a => a.Airport)
                .WithMany(ag => ag.AirportInAirportGroups);


            modelBuilder.Entity<AirportInAirportGroup>()
                .HasOne<AirportGroup>(ag => ag.AirportGroup)
                .WithMany(aag => aag.AirportInAirportGroups);

        }
        public DbSet<Country> Countries { get; set; }

        public DbSet<Airport> Airports { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<RouteWithGroup> RouteWithGroup { get; set; }

        public DbSet<AirportGroup> AirportGroups { get; set; }

        public DbSet<AirportInAirportGroup> AirportInAirportGroups { get; set; }
    }
}
