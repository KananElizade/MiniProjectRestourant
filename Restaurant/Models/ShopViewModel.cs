using Restourant.DataContext.Entities;

namespace Restourant.Models
{
    public class ShopViewModel
    {
        public List<MenuItem> MenuItems { get; set; } = [];
        public List<Category> Categories { get; set; } = [];

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
