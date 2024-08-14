using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public virtual DbSet<UserModel> Users { get; set; }
    }
}
