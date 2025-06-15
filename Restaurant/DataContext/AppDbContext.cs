using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restourant.DataContext.Entities;

namespace Restourant.DataContext
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OfflineOrder> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<ReserveTable> ReserveTables { get; set; }
        public DbSet<MenuItemImage> MenuItemImages { get; set; }


    }
}
