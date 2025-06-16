using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Restourant.DataContext.Entities
{
    public class ReserveTable
    {
        public int Id { get; set; }
        public required string GuestFirstName { get; set; }
        public required string GuestLastName { get; set; }
        public required string GuestPhoneNumber { get; set; }
        public required int TableId { get; set; }
        public Table? Table { get; set; }
        public DateTime ReservationStartTime { get; set; }
        public DateTime? ReservationEndTime { get; set; }
    }

}
