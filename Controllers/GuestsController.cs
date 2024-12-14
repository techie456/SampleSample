using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{


    public class GuestsController : Controller
    {
        private readonly HotelDbContext _context;

        public GuestsController(HotelDbContext context)
        {
            _context = context;
        }
                
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Guests.ToListAsync());
            var guests = await _context.Guests
       .Include(g => g.Reservations) // Include reservations for each guest
           .ThenInclude(r => r.Room)   // Include room details within each reservation
       .ToListAsync();

            return View(guests);
        }

        
        public IActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestID,Name,ContactInfo,Address")] Guests guest)
        {
           
                _context.Add(guest);
                await _context.SaveChangesAsync();
               // return RedirectToAction(nameof(Index));
            
            return View(guest);
        }
               
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return NotFound();

            return View(guest);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuestID,Name,ContactInfo,Address")] Guests guest)
        {
            if (id != guest.GuestID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(guest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }
                
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var guest = await _context.Guests.FirstOrDefaultAsync(g => g.GuestID == id);
            if (guest == null) return NotFound();

            return View(guest);
        }
                
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
