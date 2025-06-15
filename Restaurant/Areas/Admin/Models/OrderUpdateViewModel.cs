using Restourant.DataContext.Entities;

namespace Restourant.Areas.Admin.Models
{
    public class OrderUpdateViewModel
    {
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }
        public required Table Table { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
