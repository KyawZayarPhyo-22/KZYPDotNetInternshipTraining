using Microsoft.EntityFrameworkCore;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Models;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}