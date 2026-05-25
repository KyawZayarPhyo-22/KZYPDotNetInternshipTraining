using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelData;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomTypesController(HotelDbContext context)
        {
            _context = context;
        }

        // ၁။ GET: api/roomtypes \
        [HttpGet]
        public async Task<IActionResult> GetRoomTypes()
        {
            var roomTypes = await _context.RoomTypes.ToListAsync();

            var responseData = roomTypes.Select(rt => new RoomTypeResponse
            {
                RoomName = rt.RoomName,
                BasePrice = rt.BasePrice,
                MaxGuests = rt.MaxGuests,
                Description = rt.Description
            }).ToList();

            var response = new RoomResponse<List<RoomTypeResponse>>
            {
                IsSuccess = true,
                Message = "Room types retrieved successfully.",
                Data = responseData
            };

            return Ok(response);
        }

        // ၂။ GET: api/roomtypes/{id} (ID အလိုက် တစ်ခုချင်းစီ ရှာဖွေပြသရန်)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
            {
                return NotFound(new RoomResponse<RoomTypeResponse>
                {
                    IsSuccess = false,
                    Message = $"RoomType ID {id} ကို ရှာမတွေ့ပါ။",
                    Data = null
                });
            }

            var responseData = new RoomTypeResponse
            {
                RoomTypeId = roomType.RoomTypeId,
                RoomName = roomType.RoomName,
                BasePrice = roomType.BasePrice,
                MaxGuests = roomType.MaxGuests,
                Description = roomType.Description
            };

            return Ok(new RoomResponse<RoomTypeResponse>
            {
                IsSuccess = true,
                Message = "Room type found.",
                Data = responseData
            });
        }

        // ၃။ POST: api/roomtypes 
        [HttpPost]
        // // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoomType([FromBody] RoomTypeRequest request)
        {
            var roomType = new RoomType
            {
                RoomName = request.RoomName,
                BasePrice = request.BasePrice,
                MaxGuests = request.MaxGuests,
                Description = request.Description
            };

            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync();

            var responseData = new RoomTypeResponse
            {
                RoomTypeId = roomType.RoomTypeId,
                RoomName = roomType.RoomName,
                BasePrice = roomType.BasePrice,
                MaxGuests = roomType.MaxGuests,
                Description = roomType.Description
            };

            var response = new RoomResponse<RoomTypeResponse>
            {
                IsSuccess = true,
                Message = "Room type created successfully.",
                Data = responseData
            };

            return Ok(response);
        }

        // ၄။ PUT: api/roomtypes/{id} 
        [HttpPut("{id}")]
        // // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoomType(int id, [FromBody] RoomTypeRequest request)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
            {
                return NotFound(new RoomResponse<object>
                {
                    IsSuccess = false,
                    Message = "ပြင်ဆင်မည့် RoomType ရှာမတွေ့ပါ။",
                    Data = null
                });
            }

            roomType.RoomName = request.RoomName;
            roomType.BasePrice = request.BasePrice;
            roomType.MaxGuests = request.MaxGuests;
            roomType.Description = request.Description;

            await _context.SaveChangesAsync();

            return Ok(new RoomResponse<object>
            {
                IsSuccess = true,
                Message = "Room type updated successfully.",
                Data = null
            });
        }

        // ၅။ DELETE: api/roomtypes/{id}
        [HttpDelete("{id}")]
        // // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
            {
                return NotFound(new RoomResponse<object>
                {
                    IsSuccess = false,
                    Message = "ဖျက်သိမ်းမည့် RoomType ရှာမတွေ့ပါ။",
                    Data = null
                });
            }

            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();

            return Ok(new RoomResponse<object>
            {
                IsSuccess = true,
                Message = "Room type deleted successfully.",
                Data = null
            });
        }
    }
}