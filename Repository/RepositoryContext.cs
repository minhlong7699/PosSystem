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
        DbSet<Invoice>? Invoices { get; set; }
        DbSet<Order>? Orders { get; set; }
        DbSet<OrderItem>? OrderItems { get; set; }
        DbSet<Payment>? Payments { get; set; }
        DbSet<Product>? Products { get; set; }
        DbSet<Promotion>? Promotions { get; set; }
        DbSet<Supplier>? Suppliers { get; set; }
        DbSet<Table>? Tables { get; set; }
        DbSet<Tax>? Taxs { get; set; }
        DbSet<User>? Users { get; set; }
        DbSet<UserAuthentication>? UserAuthentications { get; set; }
        DbSet<UserRole>? UserRoles { get; set; }
    }
}
