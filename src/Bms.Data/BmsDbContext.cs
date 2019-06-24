using Bms.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bms.Data
{
    public class BmsDbContext : DbContext
    {
        public BmsDbContext(DbContextOptions<BmsDbContext> options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Business> Businesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u => u.ToTable("Users"));
            modelBuilder.Entity<Business>(b => b.ToTable("Businesses"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=bms-dev;user=root;password=");
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
