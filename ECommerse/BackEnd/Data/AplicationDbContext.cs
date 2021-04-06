using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class AplicationDbContext:DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products{get; set;}
        public DbSet<Category> Categories{get; set;}
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options)
        {
            }
    }

}