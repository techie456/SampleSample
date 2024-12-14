using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Models
{
        public class Payments
    {
        [Key]
        public int PaymentID { get; set; }
        public int ReservationID { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } // E.g., Pending, Completed, Cancelled

        // Navigation property for Reservation
        public Reservations Reservation { get; set; }
    }

}
