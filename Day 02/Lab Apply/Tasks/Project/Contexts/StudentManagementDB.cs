using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Contexts
{
    public class StudentManagementDB : DbContext
    {
        public StudentManagementDB(DbContextOptions<StudentManagementDB> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
