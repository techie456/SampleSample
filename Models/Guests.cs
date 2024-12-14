using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models


{
    public class Guests
    {
            [Key]
            public int GuestID { get; set; }
            public string Name { get; set; }
            public string ContactInfo { get; set; }
            public string Address { get; set; }

            // Navigation property for Reservations
            public ICollection<Reservations> Reservations { get; set; }
        }

    }

