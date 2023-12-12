using EntityFrameWorkWithXunit.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkWithXunit.Application
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {

        }
        public StudentDbContext()
        {

        }
        public DbSet<Student> students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(x =>
            {
                x.HasKey(k => k.StudentId);
                x.Property(p => p.StudentId).HasColumnName("pk_StudentId");
                x.Property(p => p.Name).HasMaxLength(50).IsRequired();
                x.Property(p => p.DateOfBirth).IsRequired();
                x.Property(p => p.Mobile).HasMaxLength(50).IsRequired(false);
            });
        }
    }
}
