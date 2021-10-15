using ConsoleApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Domain
{
    public class ConsoleAppDbContext : DbContext
    {
        public DbSet<UserInputLog> UserInputLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString =
                "Data Source=localhost;Initial Catalog=ConsoleAppDatabase;Integrated Security=False;User Id=sa;Password=Password123;Pooling=False;";
            optionsBuilder.UseSqlServer(connectionString, builder => { });
        }
    }
}
