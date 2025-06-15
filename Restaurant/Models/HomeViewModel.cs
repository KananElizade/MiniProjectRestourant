using Restourant.DataContext.Entities;

namespace Restourant.Models
{
    public class HomeViewModel
    {
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
