using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Action = WebCalculator2.Entity.Action;

namespace WebCalculator2

{
    
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext()
        {
            Database.Migrate();
        }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ApplicationDbContext>()
                .Build();

            var connectionString = configuration["ConnectionString:EfCoreBasicDatabase"];

            optionsBuilder
                .UseSqlServer(connectionString);
        }

        public DbSet<Action> Actions { get; set; }

    }
}
