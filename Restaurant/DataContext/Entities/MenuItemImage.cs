namespace Restourant.DataContext.Entities
{
    public class MenuItemImage
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int ProductId { get; set; }
        public MenuItem? menuItems { get; set; }
    }
}
