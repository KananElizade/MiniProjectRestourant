
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restourant.Areas.Admin.Data;
using Restourant.DataContext;
using Restourant.DataContext.Entities;
using Restourant.Areas.Admin.Models;
using Restourant.DataContext.Entities;
using Restourant.DataContext;
using Restaurant.Controllers;
using Restourant.Areas.Admin.Extensions;

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


        public async Task<IActionResult> Index()
        {
            var MenuItems = await _dbContext.MenuItems
                .Include(x => x.Category)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return View(MenuItems);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _dbContext.MenuItems
                .Include(x => x.Category)
                .Include(x => x.Images)
                .SingleOrDefaultAsync(x => x.Id == id);

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var categoryListItems = categories.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

            var productCreateModel = new MenuItemCreateViewModel
            {
                Name = "",
                CategorySelectListItems = categoryListItems,
                ImagesFiles = null,
                Price = 0,
                ImageUrl = "",
                Description = "",

            };

            return View(productCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemCreateViewModel model)
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var categoryListItems = categories.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();



            if (!ModelState.IsValid)
            {
                model.CategorySelectListItems = categoryListItems;

                return View(model);
            }

            if (!model.ImagesFiles.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Sekil secilmelidir!");
                model.CategorySelectListItems = categoryListItems;

                return View(model);
            }

            if (!model.ImagesFiles.IsAllowedSize(1))
            {
                ModelState.AddModelError("ImageFile", "Sekil hecmi 1mb-dan cox ola bilmez");
                model.CategorySelectListItems = categoryListItems;

                return View(model);
            }

            var productImages = new List<MenuItemImage>();

            bool isValidImages = true;

            foreach (var item in model.ImageFiles ?? [])
            {
                if (!item.IsImage())
                {
                    isValidImages = false;
                    ModelState.AddModelError("", $"{item.FileName}-sekil olmalidir");
                }

                if (!item.IsAllowedSize(1))
                {
                    isValidImages = false;
                    ModelState.AddModelError("", $"{item.FileName}-hecmi 1 mb-dan cox olmamalidir");
                }
            }

            if (!isValidImages)
            {
                model.CategorySelectListItems = categoryListItems;

                return View(model);
            }

            foreach (var item in model.ImageFiles ?? [])
            {
                var unicalFileName = await item.GenerateFile(FilePathConstants.MenuIteamPath);
                productImages.Add(new MenuItemImage { Name = unicalFileName });
            }

            var unicalCoverImageFileName = await model.ImagesFiles.GenerateFile(FilePathConstants.MenuIteamPath);

            var product = new MenuItem
            {
                Name = model.Name,
                Price = model.Price,
                Images = productImages,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };

            await _dbContext.MenuItems.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RequestModel requestModel)
        {
            var product = await _dbContext.MenuItems
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == requestModel.Id);

            if (product == null) return NotFound();

            var removedProduct = _dbContext.MenuItems.Remove(product);
            await _dbContext.SaveChangesAsync();

            if (removedProduct != null)
            {
                System.IO.File.Delete(Path.Combine(FilePathConstants.MenuIteamPath, product.ImageUrl));

                foreach (var item in product.Images)
                {
                    System.IO.File.Delete(Path.Combine(FilePathConstants.MenuIteamPath, item.Name));
                }
            }

            return Json(removedProduct.Entity);
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _dbContext.MenuItems
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);

            var categories = await _dbContext.Categories.ToListAsync();
            var categoryListItems = categories.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            if (product == null) return NotFound();

            var updateViewModel = new MenuItemUpdateViewModel
            {
                Name = product.Name,
                ImagesFiles = product.ImagesFiles,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategorySelectListItems = categoryListItems,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
            };

            return View(updateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MenuItemUpdateViewModel model)
        {
            var product = await _dbContext.MenuItems
                .Include(x => x.Name)
                .Include(x=>x.Images)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (product == null) return NotFound();

            var categories = await _dbContext.Categories.ToListAsync();
            var categoryListItems = categories.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

     


            if (!ModelState.IsValid)
            {
                model.CategorySelectListItems = categoryListItems;
                return View(model);
            }

            product.Name = model.Name;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            if (model.ImagesFiles != null)
            {
                if (!model.ImagesFiles.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Sekil secilmelidir!");
                    model.CategorySelectListItems = categoryListItems;

                    return View(model);
                }

                if (!model.ImagesFiles.IsAllowedSize(1))
                {
                    ModelState.AddModelError("ImageFile", "Sekil hecmi 1mb-dan cox ola bilmez");
                    model.CategorySelectListItems = categoryListItems;

                    return View(model);
                }

                var unicalCoverImageFileName = await model.ImagesFiles.GenerateFile(FilePathConstants.MenuIteamPath);

                if (product.ImageUrl != null)
                {
                    System.IO.File.Delete(Path.Combine(FilePathConstants.MenuIteamPath, product.ImageUrl));
                }

                product.ImageUrl = unicalCoverImageFileName;
            }

            bool isValidImages = true;

            foreach (var item in model.ImageFiles ?? [])
            {
                if (!item.IsImage())
                {
                    isValidImages = false;
                    ModelState.AddModelError("", $"{item.FileName}-sekil olmalidir");
                }

                if (!item.IsAllowedSize(1))
                {
                    isValidImages = false;
                    ModelState.AddModelError("", $"{item.FileName}-hecmi 1 mb-dan cox olmamalidir");
                }
            }

            if (!isValidImages)
            {
                model.CategorySelectListItems = categoryListItems;

                return View(model);
            }

            foreach (var item in model.ImageFiles ?? [])
            {
                var unicalFileName = await item.GenerateFile(FilePathConstants.MenuIteamPath);
                product.Images.Add(new MenuItemImage { Name = unicalFileName });
            }

            _dbContext.MenuItems.Update(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage([FromBody] RequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.ImageName)) return BadRequest();

            var productImage = await _dbContext.MenuItemImages.FirstOrDefaultAsync(x => x.Name == requestModel.ImageName);

            if (productImage == null) return BadRequest();

            var removedImage = _dbContext.MenuItemImages.Remove(productImage);
            await _dbContext.SaveChangesAsync();

            if (removedImage != null)
            {
                System.IO.File.Delete(Path.Combine(FilePathConstants.MenuIteamPath, requestModel.ImageName));
            }

            return Json(removedImage.Entity);
        }


    }
}
