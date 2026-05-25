using System;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels
{
    using System;

    namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels
    {
        public class PaymentRequest
        {
            public int PaymentId { get; set; }
            public int BookingId { get; set; }
            public decimal TotalAmount { get; set; } // ✅ Amount မှ TotalAmount သို့ ပြင်ဆင်ပြီး
            public string PaymentMethod { get; set; } = null!;
            public string TransactionId { get; set; } = null!;
            public string TransactionSsPath { get; set; } = null!;
            public string Status { get; set; } = null!; // ✅ PaymentStatus မှ Status သို့ ပြင်ဆင်ပြီး
            public DateTime PaymentDate { get; set; }
        }
  
            public class PaymentResponse
            {
            public int PaymentId { get; set; }
            public int BookingId { get; set; }
            public decimal TotalAmount { get; set; }
            public string PaymentMethod { get; set; } = null!;
            public string TransactionId { get; set; } = null!;
            public string TransactionSsPath { get; set; } = null!;
            public string Status { get; set; } = null!;
            public DateTime PaymentDate { get; set; }
        }
        }
    }