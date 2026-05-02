using System.ComponentModel.DataAnnotations;
using Project.Contexts;
using Project.Models;
using Project.Repositories;

namespace Project.Validators
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var departmentRepository = (IBaseRepository<Department>)validationContext.GetService(typeof(IBaseRepository<Department>))!;
            var dept = validationContext.ObjectInstance as Department;
            if (dept == null || string.IsNullOrWhiteSpace(dept.Name))
                return ValidationResult.Success;
            if (departmentRepository.GetByAttribute(dept.Id, d => d.Id, false).Any(d => d.Name == dept.Name))
                return new ValidationResult("Name must be unique.");
            return ValidationResult.Success;
        }
    }
}
