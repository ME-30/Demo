using Demo.EF.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.EF.DataBase
{
    public class ITIEntity :IdentityDbContext<ApplicationUsrer>
    {
        public ITIEntity()
        {

        }
        public ITIEntity(DbContextOptions options) : base(options)  
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Emp> emp { get; set; }
    }
}
