using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelData;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels;
using KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Models.GuestCreate;

namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public GuestsController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuests()
        {
            var guests = await _context.Guests.ToListAsync();

            var response = guests.Select(g => new GuestResponse
            {
                GuestId = g.GuestId,
                GuestName = g.GuestName,
                Phone = g.Phone,
                Nrc = g.Nrc,
                CreatedDateTime = g.CreatedDateTime
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return NotFound($"Guest ID {id} ကို ရှာမတွေ့ပါ။");

            var response = new GuestResponse
            {
                GuestId = guest.GuestId,
                GuestName = guest.GuestName,
                Phone = guest.Phone,
                Nrc = guest.Nrc,
                CreatedDateTime = guest.CreatedDateTime
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuest([FromBody] GuestRequest request)
        {
            var guest = new HotelModels.Guest
            {
                GuestName = request.GuestName,
                Phone = request.Phone,
                Nrc = request.Nrc,
                CreatedDateTime = DateTime.Now 
            };

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            var response = new GuestResponse
            {
                GuestId = guest.GuestId,
                GuestName = guest.GuestName,
                Phone = guest.Phone,
                Nrc = guest.Nrc,
                CreatedDateTime = guest.CreatedDateTime
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody] GuestRequest request)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return NotFound("ပြင်ဆင်မည့် ဧည့်သည်မှတ်တမ်း ရှာမတွေ့ပါ။");

            guest.GuestName = request.GuestName;
            guest.Phone = request.Phone;
            guest.Nrc = request.Nrc;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return NotFound();

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}