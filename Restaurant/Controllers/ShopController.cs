using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.DataContext;

namespace Restaurant.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCount = await _dbContext.MenuItems.CountAsync();

            var MenuItems = await _dbContext.MenuItems.Take(6).ToListAsync();

            return View(MenuItems);
        }

        //[HttpPost]
        //public async Task<IActionResult> Partial([FromBody]RequestModel requestModel)
        //{
        //    var MenuItems = await _dbContext.MenuItems.Skip(requestModel.StartFrom).Take(6).ToListAsync();

        //    return PartialView("_ProductPartialView", MenuItems);
        //}

        [HttpPost]
        public async Task<IActionResult> Partial([FromBody] RequestModel requestModel)
        {
            var MenuItems = await _dbContext.MenuItems.Skip(requestModel.StartFrom).Take(6).ToListAsync();

            return Json(MenuItems);
        }

    }

    public class RequestModel
    {
        public int Id { get; set; }
        public int StartFrom { get; set; }
        public string? ImageName { get; set; }
    }
}
