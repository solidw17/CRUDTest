using CRUDTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDTest.Context
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }

        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cidade>()
                .HasMany(c => c.Pessoas)
                .WithOne(p => p.Cidade)
                .HasForeignKey(p => p.Id_Cidade)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
