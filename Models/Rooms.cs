using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Models
{
    public class Rooms
    {
        [Key]
            public int RoomID { get; set; }
            public string RoomType { get; set; } // E.g., Single, Double, Suite
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }
        public string AvailabilityStatus { get; set; } // E.g., Available, Occupied

            // Navigation property for Reservations
            public ICollection<Reservations> Reservations { get; set; }
        }

    }

