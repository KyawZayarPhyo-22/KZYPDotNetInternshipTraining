namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.HotelModels
{
    public class RoomRequest
    {
        public string RoomNumber { get; set; } = null!;
        public int RoomTypeId { get; set; }
        public string status { get; set; } = null!;
    }
    public class RoomResponse<T>
    {
        public bool IsSuccess { get; set; }     
        public string Message { get; set; } = null!; 
        public T? Data { get; set; }            
    }
}