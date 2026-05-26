using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelData;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels.KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Controllers
{
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public PaymentsController(HotelDbContext context)
        {
            _context = context;
        }

  
        [HttpPost("/api/payments/submit")]
        public async Task<IActionResult> SubmitPayment([FromBody] PaymentRequest request)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookinhId == request.BookingId);
            if (booking == null) return NotFound("ဘွတ်ကင်နံပါတ် ရှာမတွေ့ပါ။");

            var payment = new Payment
            {
                BookingId = request.BookingId,
                TotalAmount = request.TotalAmount,
                PaymentMethod = request.PaymentMethod,
                TransactionId = request.TransactionId,
                TransactionSsPath = request.TransactionSsPath,
                //Status = "Pending",
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            var response = new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                BookingId = payment.BookingId,
                TotalAmount = payment.TotalAmount,
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId,
                TransactionSsPath = payment.TransactionSsPath,
                Status = "Panding",
                PaymentDate = payment.PaymentDate
            };

            return Ok(response);
        }

     
        [HttpPut("/api/admin/payments/{id}/verify")]
        //[Authorize("Admin")]
        public async Task<IActionResult> VerifyPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound("ငွေပေးချေမှုမှတ်တမ်း ရှာမတွေ့ပါ။");

            payment.Status = "Approved";

            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookinhId == payment.BookingId);
            if (booking != null)
            {
                booking.BookingStatus = "Confirmed";
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "ငွေလွှဲမှု အတည်ပြုပြီးပါပြီ။ Booking ကို Confirmed ပြောင်းလဲလိုက်ပါပြီ။" });
        }

        [HttpPost("/api/bookings/{id}/checkout")]
        public async Task<IActionResult> CheckoutBooking(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookinhId == id);
            if (booking == null) return NotFound("ဘွတ်ကင်မှတ်တမ်း ရှာမတွေ့ပါ။");

            booking.BookingStatus = "CheckedOut";

            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room != null)
            {
                room.Status = "Available";
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Checkout လုပ်ငန်းစဉ် အောင်မြင်ပါပြီ။ အခန်းကို Available ပြန်ပြောင်းပေးလိုက်ပါပြီ။" });
        }
    }
}