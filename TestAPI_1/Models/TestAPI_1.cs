using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TestAPI_1.Models;

namespace TestAPI_1.Models
{
    public class TestAPI_1DBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        
        public TestAPI_1DBContext(DbContextOptions<TestAPI_1DBContext> options, IConfiguration configuration) 
            : base(options) 
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectString = Configuration.GetConnectionString("CustomerDataService");
            options.UseMySql(connectString, ServerVersion.AutoDetect(connectString));
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
    }
}
