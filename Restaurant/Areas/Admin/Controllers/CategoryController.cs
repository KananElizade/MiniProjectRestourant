
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.DataContext;
using Restourant.DataContext.Entities;
using Restourant.DataContext.Entities;
using Restourant.DataContext;
using Restaurant.Areas.Admin.Controllers;

namespace Restourant.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        public IActionResult Edit(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }


        public IActionResult Details(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null) return NotFound();

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
