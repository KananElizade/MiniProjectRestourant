using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Restourant.Models
{
    public class BookTableViewModel
    {
       
            [Required]
            public string CustomerName { get; set; }

            [Required]
            [Phone]
            public required string Phone { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            public DateTime BookingDate { get; set; }

            [Range(1, 20)]
            public int NumberOfGuests { get; set; }
    }
}
