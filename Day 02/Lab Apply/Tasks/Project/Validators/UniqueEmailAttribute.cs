using System.ComponentModel.DataAnnotations;
using Project.Contexts;
using Project.Models;

namespace Project.Validators
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public UniqueEmailAttribute()
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (StudentManagementDB)validationContext.GetService(typeof(StudentManagementDB))!;
            var std = validationContext.ObjectInstance as Student;
            if (std == null || string.IsNullOrWhiteSpace(std.Email))
                return ValidationResult.Success;
            if (_context.Students.Where(s => s.Ssn != std.Ssn).Any(s => s.Email == std.Email))
               return new ValidationResult("Email must be unique.");
            return ValidationResult.Success;
        }
    }
}
