using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelData;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();

            var responseData = rooms.Select(r => new RoomResponse
            {
                RoomId = r.RoomId,
                RoomNumber = r.RoomNumber,
                RoomTypeId = r.RoomTypeId,
                status = r.Status
            }).ToList();

            var response = new RoomResponse<List<RoomResponse>>
            {
                IsSuccess = true,
                Message = "Rooms retrieved successfully.",
                Data = responseData
            };

            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateRoom([FromBody] RoomRequest request)
        {
            var room = new Room
            {
                RoomNumber = request.RoomNumber,
                RoomTypeId = request.RoomTypeId,
                Status = request.status
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            var responseData = new RoomResponse
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                RoomTypeId = room.RoomTypeId,
                status = room.Status
            };

            var response = new RoomResponse<RoomResponse>
            {
                IsSuccess = true,
                Message = "Room created successfully.",
                Data = responseData
            };

            return Ok(response);
        }

        // ၃။ PUT: api/rooms/{RoomId} 
        [HttpPut("{RoomId}")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateRoom(int RoomId, [FromBody] RoomRequest request)
        {
            var room = await _context.Rooms.FindAsync(RoomId);

            if (room == null)
            {
                return NotFound(new RoomResponse<object>
                {
                    IsSuccess = false,
                    Message = "ပြင်ဆင်မည့် Room ရှာမတွေ့ပါ။",
                    Data = null
                });
            }

            room.RoomNumber = request.RoomNumber;
            room.RoomTypeId = request.RoomTypeId;
            room.Status = request.status;

            await _context.SaveChangesAsync();

            return Ok(new RoomResponse<object>
            {
                IsSuccess = true,
                Message = "Room updated successfully.",
                Data = null
            });
        }

        // ၄။ DELETE: api/rooms/{RoomId} 
        [HttpDelete("{RoomId}")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteRoom(int RoomId)
        {
            var room = await _context.Rooms.FindAsync(RoomId);

            if (room == null)
            {
                return NotFound(new RoomResponse<object>
                {
                    IsSuccess = false,
                    Message = "ဖျက်သိမ်းမည့် Room ရှာမတွေ့ပါ။",
                    Data = null
                });
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Ok(new RoomResponse<object>
            {
                IsSuccess = true,
                Message = "Room deleted successfully.",
                Data = null
            });
        }

        // ၅။ GET: api/rooms/available 
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut)
        {
            var occupiedRoomIds = await _context.Bookings
                .Where(b => b.BookingStatus == "Confirmed" || b.BookingStatus == "Pending_Approval")
                .Where(b => !(checkOut <= b.CheckInDate || checkIn >= b.CheckOutDate))
                .Select(b => b.RoomId)
                .Distinct()
                .ToListAsync();

            var allRooms = await _context.Rooms
                .Include(r => r.RoomType)
                .ToListAsync();

            var roomStatusList = allRooms.Select(r => new
            {
                r.RoomId,
                r.RoomNumber,
                r.RoomTypeId,
                r.Status,
                RoomName = r.RoomType?.RoomName,
                BasePrice = r.RoomType?.BasePrice,
                IsAvailableForBooking = !occupiedRoomIds.Contains(r.RoomId) && r.Status == "Available"
            }).ToList();

            var response = new RoomResponse<object>
            {
                IsSuccess = true,
                Message = "Available rooms status retrieved successfully.",
                Data = roomStatusList
            };

            return Ok(response);
        }
    }
}