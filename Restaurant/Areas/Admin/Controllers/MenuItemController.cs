
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restourant.Areas.Admin.Data;
using Restourant.DataContext;
using Restourant.DataContext.Entities;
using Restourant.Areas.Admin.Models;
using Restourant.DataContext.Entities;
using Restourant.DataContext;

namespace Restourant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public MenuItemController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            var menuItems = _dbContext.MenuItems
                .Include(m => m.Category)
                .ToList();

            return View(menuItems);
        }


        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dbContext.Categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        public IActionResult Create(MenuItemCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _dbContext.Categories.ToList();
                return View(model);
            }

            var menuItem = new MenuItem
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                IsAvaliable = model.IsAvailable,
                CategoryId = model.CategoryId
            };

            _dbContext.MenuItems.Add(menuItem);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var menuItem = _dbContext.MenuItems.Find(id);
            if (menuItem == null) return NotFound();

            var model = new MenuItemUpdateViewModel
            {
                CategoryId = menuItem.CategoryId,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                ImageUrl = menuItem.ImageUrl,
                IsAvailable = menuItem.IsAvaliable,
                Categories = _dbContext.Categories.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, MenuItemUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _dbContext.Categories.ToList();
                return View(model);
            }

            var menuItem = _dbContext.MenuItems.Find(id);
            if (menuItem == null) return NotFound();

            // Resim yükleme kısmı artık yok

            menuItem.Name = model.Name;
            menuItem.Description = model.Description;
            menuItem.Price = model.Price;
            menuItem.CategoryId = model.CategoryId;
            menuItem.IsAvaliable = model.IsAvailable;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        // Detay
        public IActionResult Details(int id)
        {
            var menuItem = _dbContext.MenuItems
                .Include(m => m.Category)
                .FirstOrDefault(m => m.Id == id);

            if (menuItem == null) return NotFound();

            return View(menuItem);
        }


        public IActionResult Delete(int id)
        {
            var menuItem = _dbContext.MenuItems.Find(id);
            if (menuItem == null) return NotFound();

            _dbContext.MenuItems.Remove(menuItem);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
