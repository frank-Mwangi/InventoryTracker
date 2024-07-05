using InventoryTracker.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Items> Items { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
