using ContentPlatformInterview.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace ContentPlatformInterview.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
