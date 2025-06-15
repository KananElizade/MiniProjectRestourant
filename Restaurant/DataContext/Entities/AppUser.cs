using Microsoft.AspNetCore.Identity;

namespace Restourant.DataContext.Entities
{
    public class AppUser :IdentityUser
    {
        public required string FullName { get; set; }
    }
}
