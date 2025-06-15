using Restourant.DataContext.Entities;

namespace Restourant.Models
{
    public class MenuVievModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public List<MenuItem> menuItems { get; set; } = new List<MenuItem>();
    }
}
