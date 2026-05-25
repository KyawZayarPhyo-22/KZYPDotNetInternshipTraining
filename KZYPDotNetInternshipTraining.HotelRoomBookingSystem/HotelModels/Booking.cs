using System;
using System.Collections.Generic;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;

public partial class Booking
{
    public int BookinhId { get; set; }

    public int GuestId { get; set; }

    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string BookingStatus { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Room Room { get; set; } = null!;
}
