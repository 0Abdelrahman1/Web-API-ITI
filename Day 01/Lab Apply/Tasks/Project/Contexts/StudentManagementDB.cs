using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Contexts
{
    public class StudentManagementDB : DbContext
    {
        public StudentManagementDB(DbContextOptions<StudentManagementDB> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
