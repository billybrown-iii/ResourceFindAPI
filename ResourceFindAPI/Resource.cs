using Microsoft.EntityFrameworkCore;

namespace ResourceFindAPI
{
    public class Resource
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
    }

    class ResourceDb : DbContext
    {
        public ResourceDb(DbContextOptions options) : base(options) { }
        public DbSet<Resource> Resources { get; set; } = null!;
    }
}
