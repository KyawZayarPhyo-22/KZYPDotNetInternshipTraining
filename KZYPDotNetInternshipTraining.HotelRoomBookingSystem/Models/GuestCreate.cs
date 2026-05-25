namespace KZYPDotNetInternshipTraining.HotelRoomBookingSystem.Models
{
    public class GuestCreate
    {
    
        public class GuestRequest
        {
            public string GuestName { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public string Nrc { get; set; } = null!;
        }
    }
    public class GuestResponse
    {
        public int GuestId { get; set; }
        public string GuestName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Nrc { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
    }
      
}

