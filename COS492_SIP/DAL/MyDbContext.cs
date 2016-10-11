using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using COS492_SIP.Models;

namespace COS492_SIP.DAL
{
    public class MyDbContext : DbContext
    {
        
        public MyDbContext() : base("MyDbContextConnectionString")
        {
            Database.SetInitializer<MyDbContext>(new MyDbInitializer());
        }

        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}