using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Threading.Tasks;
namespace HotelManagementSystem.Controllers
{
             
public class ReservationsController : Controller
    {
        private readonly HotelDbContext _context;

        public ReservationsController(HotelDbContext context)
        {
            _context = context;
        }
                  
            
        [HttpPost]
        public async Task<IActionResult> FetchGuestInfo(Reservations reservation)
        {
            // Retrieve guest information based on GuestID
            var guest = await _context.Guests
     .Include(g => g.Reservations)
     .ThenInclude(r => r.Room)
     .Where(g => g.GuestID == reservation.GuestID)
     .Select(g => new
     {
         GuestName = g.Name,
         AssignedRoom = g.Reservations.Any() ? g.Reservations.First().Room.RoomID.ToString() : "No Room Assigned",
         AssignmentDate = g.Reservations.Any() ? g.Reservations.First().CheckIn.ToString("yyyy-MM-dd") : "N/A"
     })
     .FirstOrDefaultAsync();

            ViewBag.GuestInfo = guest ?? new { GuestName = "Guest not found", AssignedRoom = "", AssignmentDate = "" };

            
            return View("Create", reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
                   
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reservations = _context.Reservations.Include(r => r.Guest).Include(r => r.Room);
            return View(await reservations.ToListAsync());
        }

        [HttpGet]
           public IActionResult Create()
            {
                // If rooms data is provided, use it; otherwise, fetch from the database
                ViewBag.AllRooms = _context.Rooms.ToList();
           // var allRooms = await _context.Rooms.ToListAsync();
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,GuestID,RoomID,CheckIn,CheckOut")] Reservations reservation)
        {

            // _context.Add(reservation);
            // await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            //return View(reservation);|

            
                _context.Add(reservation);

                // Update the room's status to "Occupied"
                var room = await _context.Rooms.FindAsync(reservation.RoomID);
                if (room != null)
                {
                    room.AvailabilityStatus = "Occupied";
                    _context.Update(room);
                }

                await _context.SaveChangesAsync();

                // Set success message
                TempData["SuccessMessage"] = "Reservation done successfully!";
               // return RedirectToAction(nameof(GetCreateReservation));
            

            // Fetch all rooms again if ModelState is invalid
            ViewBag.AllRooms = await _context.Rooms.ToListAsync();
            return View(reservation);
        



    }
        [HttpGet]
        public async Task<IActionResult> GetCreateReservation()
        {
            // Fetch all room details and pass to the view
            //ViewBag.AllRooms = await _context.Rooms.ToListAsync();
            var allRooms = await _context.Rooms.ToListAsync();

            // Redirect to Create action and pass the room list as a model
            return RedirectToAction("Create", "Reservations", new { rooms = allRooms });
            
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            return View(reservation);
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,GuestID,RoomID,CheckIn,CheckOut")] Reservations reservation)
        {
            if (id != reservation.ReservationID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }
    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null) return NotFound();

            return View(reservation);
        }
    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}

