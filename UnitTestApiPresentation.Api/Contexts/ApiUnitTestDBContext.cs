using Microsoft.EntityFrameworkCore;
using UnitTestApiPresentation.Api.Models;

namespace UnitTestApiPresentation.Api.Contexts
{
    public partial class ApiUnitTestDBContext : DbContext
    {
        public ApiUnitTestDBContext()
        {
        }

        public ApiUnitTestDBContext(DbContextOptions<ApiUnitTestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
