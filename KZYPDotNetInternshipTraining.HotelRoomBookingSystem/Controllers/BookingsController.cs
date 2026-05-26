using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelData;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Controllers
{
    public class BookingPatchRequest
    {
        public string BookingStatus { get; set; } = null!;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public BookingsController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookinh(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookinhId == id);

            if (booking == null)
            {
                return NotFound(new RoomResponse<BookingResponse>
                {
                    IsSuccess = false,
                    Message = "Booking ရှာမတွေ့ပါ။",
                    Data = null
                });
            }

            var responseData = new BookingResponse
            {
                BookinhId = booking.BookinhId,
                RoomId = booking.RoomId,
                GuestId = booking.GuestId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalAmount = booking.TotalAmount,
                BookingStatus = booking.BookingStatus,
                CreateDateTime = booking.CreateDateTime
            };

            return Ok(new RoomResponse<BookingResponse>
            {
                IsSuccess = true,
                Message = "Booking details retrieved successfully.",
                Data = responseData
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == request.RoomId);
            if (room == null)
            {
                return BadRequest(new RoomResponse<object> { IsSuccess = false, Message = "ရွေးချယ်ထားသော အခန်းမရှိပါ။", Data = null });
            }

            if (room.Status != "Available")
            {
                return BadRequest(new RoomResponse<object> { IsSuccess = false, Message = "ဤအခန်းသည် လောလောဆယ် မအားပါ။", Data = null });
            }

            int totalNights = (request.CheckOutDate - request.CheckInDate).Days;
            if (totalNights <= 0)
            {
                return BadRequest(new RoomResponse<object> { IsSuccess = false, Message = "ထွက်ခွာမည့်ရက်သည် ရောက်ရှိမည့်ရက်ထက် ပိုကြီးရပါမည်။", Data = null });
            }

            var roomType = await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.RoomTypeId == room.RoomTypeId);
            if (roomType == null)
            {
                return BadRequest(new RoomResponse<object> { IsSuccess = false, Message = "ဤအခန်း၏ အမျိုးအစား စျေးနှုန်းဒေတာ ရှာမတွေ့ပါ။", Data = null });
            }

            var booking = new Booking
            {
                RoomId = request.RoomId,
                GuestId = request.GuestId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                TotalAmount = totalNights * roomType.BasePrice,
                BookingStatus = "Pending_Approval",
                CreateDateTime = DateTime.Now
            };

            _context.Bookings.Add(booking);
            room.Status = "Occupied";
            await _context.SaveChangesAsync();

            var responseData = new BookingResponse
            {
                BookinhId = booking.BookinhId,
                RoomId = booking.RoomId,
                GuestId = booking.GuestId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalAmount = booking.TotalAmount,
                BookingStatus = booking.BookingStatus,
                CreateDateTime = booking.CreateDateTime
            };

            return Ok(new RoomResponse<BookingResponse>
            {
                IsSuccess = true,
                Message = "Booking created successfully and room status updated to Occupied.",
                Data = responseData
            });
        }



        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookinhId == id);

            if (booking == null)
            {
                return NotFound(new RoomResponse<object> { IsSuccess = false, Message = "ဖျက်သိမ်းမည့် Booking ရှာမတွေ့ပါ။", Data = null });
            }

            booking.BookingStatus = "Cancelled";

            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room != null) room.Status = "Available";

            await _context.SaveChangesAsync();

            return Ok(new RoomResponse<object>
            {
                IsSuccess = true,
                Message = "Booking cancelled successfully and room is now available.",
                Data = null
            });
        }
    }
}