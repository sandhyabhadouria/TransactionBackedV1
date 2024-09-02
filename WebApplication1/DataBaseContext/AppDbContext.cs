using Microsoft.EntityFrameworkCore;
using TransactionSystem.Models;

namespace TransactionSystem.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
    }
}
