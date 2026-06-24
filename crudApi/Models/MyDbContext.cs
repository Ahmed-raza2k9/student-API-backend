using Microsoft.EntityFrameworkCore;
namespace crudApi.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
       public DbSet<Student> Students { get; set; }

    }
}
