using Microsoft.EntityFrameworkCore;
using OrderManagement.Models;

namespace OrderManagement
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) 
        {
        
        }

        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<ProductModel> Products { get; set; }
        public virtual DbSet<OrderModel> Orders { get; set; }
        public virtual DbSet<OrderProductModel> OrderProducts { get; set; }
    }
}
