using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Models
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public int MaxGuests { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
    }

    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public string status { get; set; } = "Available";

        [ForeignKey("RoomTypeId")]
        public RoomType? RoomType { get; set; }
    }

    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string NRC { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }

    public class Booking
    {
        [Key]
        public int BookinhId { get; set; }
        public int GuestId { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingStatus { get; set; } = "Pending_Approval";
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        [ForeignKey("GuestId")]
        public Guest? Guest { get; set; }

        [ForeignKey("RoomID")]
        public Room? Room { get; set; }
    }

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string TransactionSsPath { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }
    }
}