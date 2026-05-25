using System;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels
{
    public class BookingRequest
    {
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }

    public class BookingResponse
    {
        public int BookinhId { get; set; } 
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingStatus { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
    }
}