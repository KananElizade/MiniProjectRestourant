using Restourant.DataContext.Entities;
using Restourant.DataContext.Entities;

namespace Restourant.Areas.Admin.Models
{
    public class ReserveTableCreateViewModel
    {
        public int TableId { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public required List<ReserveTable> ReserveTables { get; set; }


    }
}
