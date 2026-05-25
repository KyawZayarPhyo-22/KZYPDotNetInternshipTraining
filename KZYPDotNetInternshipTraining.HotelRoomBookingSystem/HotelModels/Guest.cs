using System;
using System.Collections.Generic;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;

public partial class Guest
{
    public int GuestId { get; set; }

    public string GuestName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Nrc { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
