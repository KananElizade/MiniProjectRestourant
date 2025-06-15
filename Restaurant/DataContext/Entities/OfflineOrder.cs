namespace Restourant.DataContext.Entities
{
    public class OfflineOrder
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }
        public required Table Table { get; set; }
        public List<OrderItem> OrderItems { get; set; } 

    }
}
