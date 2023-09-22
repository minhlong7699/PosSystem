using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Category>? Categories { get; set; }
        DbSet<Invoice>? Invoice { get; set; }
        DbSet<Order>? Order { get; set; }
        DbSet<OrderItem>? OrderItem { get; set; }
        DbSet<Payment>? Payment { get; set; }
        DbSet<Product>? Product { get; set; }
        DbSet<Promotion>? Promotion { get; set; }
        DbSet<Supplier>? Supplier { get; set; }
        DbSet<Table>? Table { get; set; }
        DbSet<Tax>? Tax { get; set; }
        DbSet<User>? User { get; set; }
        DbSet<UserAuthentication>? UserAuthentication { get; set; }
        DbSet<UserRole>? UserRole { get; set; }
    }
}
