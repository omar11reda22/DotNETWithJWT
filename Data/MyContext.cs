using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace practise_JWT.Data
{
    public class MyContext:IdentityDbContext<Applicationuser>
    {
        public MyContext()
        {
                
        }
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=JWT-pract;Integrated Security=True;TrustServerCertificate=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
