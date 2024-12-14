using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Models
{
    

public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public DbSet<Guests> Guests { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Payments> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* modelBuilder.Entity<Reservations>()
                 .HasOne(r => r.Guest)
                 .WithMany(g => g.Reservations)
                 .HasForeignKey(r => r.GuestID);

             modelBuilder.Entity<Reservations>()
                 .HasOne(r => r.Room)
                 .WithMany(rm => rm.Reservations)
                 .HasForeignKey(r => r.RoomID);

             modelBuilder.Entity<Payments>()
                 .HasOne(p => p.Reservation)
                 .WithMany(r => r.Payments)
                 .HasForeignKey(p => p.ReservationID);



             modelBuilder.Entity<Guests>()
         .HasKey(g => g.GuestID); // Define GuestID as the primary key for Guests

             // Add additional entity configurations if needed
             modelBuilder.Entity<Reservations>()
                 .HasOne(r => r.Guest)
                 .WithMany(g => g.Reservations)
                 .HasForeignKey(r => r.GuestID);

             modelBuilder.Entity<Reservations>()
                 .HasOne(r => r.Room)
                 .WithMany(rm => rm.Reservations)
                 .HasForeignKey(r => r.RoomID);

             modelBuilder.Entity<Payments>()
                 .HasOne(p => p.Reservation)
                 .WithMany(r => r.Payments)
                 .HasForeignKey(p => p.ReservationID);*/
            modelBuilder.Entity<Rooms>()
         .HasKey(r => r.RoomID);  // Define RoomID as the primary key for Rooms
            modelBuilder.Entity<Payments>()
       .Property(p => p.Amount)
       .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Rooms>()
        .Property(r => r.Rate)
        .HasColumnType("decimal(18, 2)");

            // Other entity configurations
            modelBuilder.Entity<Reservations>()
                .HasOne(r => r.Guest)
                .WithMany(g => g.Reservations)
                .HasForeignKey(r => r.GuestID);

            modelBuilder.Entity<Reservations>()
                .HasOne(r => r.Room)
                .WithMany(rm => rm.Reservations)
                .HasForeignKey(r => r.RoomID);

            modelBuilder.Entity<Payments>()
                .HasOne(p => p.Reservation)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ReservationID);
        }
    }

}

