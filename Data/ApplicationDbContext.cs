using System;
using bookingPlatform.Models;
using COMP2139_Assignment1.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Hotels> Hotels { get; set; }

		public DbSet<Flights> Flights { get; set; }

		public DbSet<Cars> Cars { get; set; }


        public DbSet<GuestUsers> Guest { get; set; }

        public DbSet<Cart> CartItems { get; set; }

        public DbSet<FlightCart> FlightCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FlightCart>()
             .HasKey(fc => new { fc.FlightID, fc.CartID }); // Configure composite primary key for flight cart junction table

            modelBuilder.Entity<FlightCart>()
                .HasOne(fc => fc.Flight)
                .WithMany(f => f.FlightCarts)
                .HasForeignKey(fc => fc.FlightID); // Configure foreign key for Flight

            modelBuilder.Entity<FlightCart>()
                .HasOne(fc => fc.Cart)
                .WithMany(c => c.FlightCarts)
                .HasForeignKey(fc => fc.CartID); // Configure foreign key for Cart

            modelBuilder.Entity<Hotels>()
                .HasOne(h => h.Cart)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h => h.CartID); // Configure foreign key for Cart in Hotel (hotel room)
        }
    }
}

