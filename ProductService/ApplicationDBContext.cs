using Microsoft.EntityFrameworkCore;
using ProductsService.Models;

namespace ProductsService
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public virtual DbSet<ProductModel> Products { get; set; }
    }
}
