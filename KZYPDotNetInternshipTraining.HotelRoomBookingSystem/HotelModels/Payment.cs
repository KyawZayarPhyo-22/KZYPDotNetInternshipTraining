using System;
using System.Collections.Generic;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int BookingId { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public string TransactionSsPath { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
