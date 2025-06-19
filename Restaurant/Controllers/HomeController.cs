using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.DataContext;
using Restourant.DataContext;

namespace Restourant.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();
        }
    }
}