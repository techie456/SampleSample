using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    
    public class Reservations
    {
        [Key]
        public int ReservationID { get; set; }
        public int GuestID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        // Navigation properties
        public Guests Guest { get; set; }
        public Rooms Room { get; set; }
        public ICollection<Payments> Payments { get; set; }
    }

}
