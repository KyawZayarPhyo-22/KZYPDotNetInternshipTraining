namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels
{
    public class RoomTypeRequest
    {
        public string RoomName { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public int MaxGuests { get; set; }
        public string? Description { get; set; }
    }

    public class RoomTypeResponse
    {
        public int RoomTypeId { get; set; }
        public string RoomName { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public int MaxGuests { get; set; }
        public string? Description { get; set; }
    }
}