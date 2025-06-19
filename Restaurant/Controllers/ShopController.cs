
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.DataContext;
using Restourant.Models;
using Restourant.DataContext;
using Restourant.Models;

namespace Restourant.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8)
        {
            var totalItems = await _dbContext.MenuItems.CountAsync();

            var menuItems = await _dbContext.MenuItems
                .Include(x => x.Category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var categories = await _dbContext.Categories.ToListAsync();

            var viewModel = new ShopViewModel
            {
                MenuItems = menuItems,
                Categories = categories,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            };

            return View(viewModel);
        }
        public class RequestModel
        {
            public int Id { get; set; }
            public int StartFrom { get; set; }
            public string? ImageName { get; set; }
        }
    }
}
