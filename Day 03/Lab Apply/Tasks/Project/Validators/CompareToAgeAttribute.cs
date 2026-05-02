using System.ComponentModel.DataAnnotations;
using Project.Models;

namespace Project.Validators
{
    public class CompareToAgeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            DateTime dateOfBirth = (DateTime)value;
            var std = validationContext.ObjectInstance as Student;
            return Math.Floor((DateTime.Now - dateOfBirth).TotalDays / 365.25) == std.Age ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
