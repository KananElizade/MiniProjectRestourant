
using Microsoft.AspNetCore.Mvc;
using Restourant.DataContext;
using Restourant.DataContext.Entities;
using Restourant.DataContext;
using System.Linq;

namespace Restourant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TableController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TableController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            var tables = _dbContext.Tables.ToList();
            return View(tables);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Table table)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Tables.Add(table);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(table);
        }


        public IActionResult Edit(int id)
        {
            var table = _dbContext.Tables.Find(id);
            if (table == null) return NotFound();
            return View(table);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Table table)
        {
            if (id != table.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _dbContext.Tables.Update(table);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(table);
        }


        public IActionResult Details(int id)
        {
            var table = _dbContext.Tables.Find(id);
            if (table == null) return NotFound();
            return View(table);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var table = _dbContext.Tables.Find(id);
            if (table == null) return NotFound();

            _dbContext.Tables.Remove(table);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
