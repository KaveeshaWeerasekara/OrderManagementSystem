using Microsoft.EntityFrameworkCore;
using OrderProductService.Models;

namespace OrderProductService
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public virtual DbSet<OrderProductModel> OrderProducts { get; set; }
    }
}
