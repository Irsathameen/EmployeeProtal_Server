using EmployeePortal.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace EmployeePortal.Data.DataContext
{
    public class EmployeeContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Irsathameen\\SQLEXPRESS; Database=Employee;User Id=sa;password=admin@12345; Integrated Security=False; trustServerCertificate=true;");
        }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
