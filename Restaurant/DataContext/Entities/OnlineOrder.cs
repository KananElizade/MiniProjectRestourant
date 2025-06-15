using NuGet.Protocol;

namespace Restourant.DataContext.Entities
{
    public class OnlineOrder
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int AppUserId { get; set; }
        public required AppUser AppUser { get; set; }
        public required string Adress { get; set; }
        public decimal TotalAmount { get; set; }



    }
}
