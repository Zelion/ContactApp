using ContactApp.Data.Configuration;
using ContactApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactApp.Data.Context
{
    public class ContactAppContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextOptions _options;

        public ContactAppContext()
        {

        }

        public ContactAppContext(DbContextOptions<ContactAppContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
            _options = options; 
        }

        public DbSet<Contact> Contacts { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Azure_ConnectionString"), o =>
                {
                    o.EnableRetryOnFailure();
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}