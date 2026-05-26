namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Controllers
{
    internal class RoomResponse
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string status { get; set; }
        public string RoomName { get; internal set; }
    }
}