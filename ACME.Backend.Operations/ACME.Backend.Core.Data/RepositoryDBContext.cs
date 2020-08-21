using ACME.Backend.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ACME.Backend.Core.Data
{
    public class RepositoryDBContext : DbContext
    {
        public RepositoryDBContext(DbContextOptions<RepositoryDBContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }
    }
}
