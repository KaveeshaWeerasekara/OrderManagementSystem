using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public virtual DbSet<OrderModel> Orders { get; set; }
    }
}
