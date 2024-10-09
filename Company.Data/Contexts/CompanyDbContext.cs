using Company.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Company.Data.Contexts
{
    public class CompanyDbContext : IdentityDbContext<ApplicationUser>
    {
        public CompanyDbContext(DbContextOptions options):base(options) { }
        
       
        /* public CompanyDbContext(DbContextOptions options) : base(options)
         {
         }*/
        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             base.OnConfiguring(optionsBuilder);
         }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(x=>x.IsActive==true);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
