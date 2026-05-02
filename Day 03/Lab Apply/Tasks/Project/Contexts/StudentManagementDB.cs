using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Contexts
{
    public class StudentManagementDB : DbContext
    {
        public StudentManagementDB(DbContextOptions<StudentManagementDB> options) : base(options)
        {
        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .HasDefaultValue("Ahmed"); // 5 letters, matches regex

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .HasDefaultValue("student@example.com"); // should be unique per row

            modelBuilder.Entity<Student>()
                .Property(s => s.Age)
                .HasDefaultValue((byte)18); // Range(18,20)

            modelBuilder.Entity<Student>()
                .Property(s => s.DateOfBirth)
                .HasDefaultValueSql("DATEADD(year, -18, GETDATE())");

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .HasDefaultValue("General"); // Required, should be unique per row

            modelBuilder.Entity<Department>()
                .Property(d => d.Location)
                .HasDefaultValue("Main Campus"); // MaxLength(100)

            modelBuilder.Entity<Department>()
                .Property(d => d.PhoneNumber)
                .HasDefaultValue("+201234567890"); // +CC + 10 digits

            modelBuilder.Entity<Department>()
                .Property(d => d.Manager)
                .HasDefaultValue("Manager"); // 3-20 chars
        }
    }
}
