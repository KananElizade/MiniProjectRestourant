using Microsoft.AspNetCore.Mvc.Rendering;
using Restourant.DataContext.Entities;

namespace Restourant.Areas.Admin.Models
{
    public class MenuItemCreateViewModel
    {
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public List<Category>? Categories { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; } = new List<SelectListItem>();
        public IFormFile[]? ImageFile { get; set; }
        public required IFormFile CoverImageFile { get; set; }

    }
}












