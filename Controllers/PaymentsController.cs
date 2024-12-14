using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    
       

public class PaymentsController : Controller
    {
        private readonly HotelDbContext _context;

        public PaymentsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = _context.Payments.Include(p => p.Reservation);
            return View(await payments.ToListAsync());
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            //   var reservations = await _context.Reservations
            //.Include(r => r.Guest)
            // .Include(r => r.Room)   
            // .ToListAsync();

            //    return View(reservations);

            ViewBag.AllReservations = _context.Reservations
                .Include(r => r.Guest)
                .Include(r => r. Room)
                .ToList();


            return View();
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,ReservationID,Amount,PaymentDate,PaymentStatus")] Payments payment)
        {
              _context.Add(payment);
                await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
            TempData["PaymentSuccessMessage"] = "Payment done successfully!";
            ViewBag.AllReservations = _context.Reservations
               .Include(r => r.Guest)
               .Include(r => r.Room)
               .ToList();
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();

            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,ReservationID,Amount,PaymentDate,PaymentStatus")] Payments payment)
        {
            if (id != payment.PaymentID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payments
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (payment == null) return NotFound();

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}

