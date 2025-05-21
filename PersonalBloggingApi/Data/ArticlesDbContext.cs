using Microsoft.EntityFrameworkCore;
using PersonalBloggingApi.Models;

namespace PersonalBloggingApi.Data
{
    public class ArticlesDbContext : DbContext
    {
        public ArticlesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
    }
}
