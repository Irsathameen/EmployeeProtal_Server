using EmployeePortal.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace EmployeePortal.Data.DataContext
{
    public class EmployeeContext : DbContext
    {

        public EmployeeContext()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<User> T_Users { get; set; }
        public DbSet<UserRole> T_UserRole { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=Irsathameen\\SQLEXPRESS; Database=Employee;User Id=sa;password=admin@12345; Integrated Security=False; trustServerCertificate=true;");

        //}

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
