using NuGet.Protocol;

namespace Restourant.DataContext.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
