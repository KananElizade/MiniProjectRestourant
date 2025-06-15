using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.DataContext;

namespace Restourant.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }


        // READ: List all bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.ReserveTables.ToListAsync();
            return View(bookings);
        }   

        // CREATE: Get form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Post form
        [HttpPost]
        public async Task<IActionResult> Create(BookingController booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // UPDATE: Get form
        // DELETE: Get confirm page
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.ReserveTables.FindAsync(id);
            return View(booking);
        }

        // DELETE: Confirm delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.ReserveTables.FindAsync(id);
            _context.ReserveTables.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.ReserveTables.FindAsync(id);
            return View(booking);
        }
    }

}
