using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Restourant.DataContext.Entities
{
    public class ReserveTable
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public required Table Table { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }



    }
}
