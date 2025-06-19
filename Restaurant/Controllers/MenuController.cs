
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.DataContext;
using Restourant.Models;
using Restourant.DataContext;
using Restourant.Models;

namespace RestaurantMVC.Controllers
{
    public class MenuController : Controller
    {

        private readonly AppDbContext _dbContext;

        public MenuController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var menuItems = await _dbContext.MenuItems
                .Where(x => x.IsAvaliable)
                .Include(x => x.Category)
                .ToListAsync();

            var categories = await _dbContext.Categories.ToListAsync();

            var viewModel = new ShopViewModel
            {
                MenuItems = menuItems,
                Categories = categories,
                CurrentPage = 1,
                TotalPages = 1
            };

            return View(viewModel);
        }

    }
}
