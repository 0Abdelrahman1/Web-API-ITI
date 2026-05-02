using System.ComponentModel.DataAnnotations;
using Project.Contexts;
using Project.Models;
using Project.Repositories;

namespace Project.Validators
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        //private readonly string _attributeName;
        //private readonly string _keyName;
        //public UniqueEmailAttribute(string attributeName, string keyName)
        //{
        //    _attributeName = attributeName;
        //    _keyName = keyName;
        //}
        //protected ValidationResult? IsValid(T? value, ValidationContext validationContext)
        //{
        //    var repository = (IBaseRepository<T>)validationContext.GetService(typeof(IBaseRepository<>).MakeGenericType(typeof(T)))!;
        //    var table = (T?)validationContext.ObjectInstance;
        //    var propertyValue = (TProp?)table?.GetType().GetProperty(_attributeName)?.GetValue(table);
        //    var keyValue = (TKey?)table?.GetType().GetProperty(_keyName)?.GetValue(table);
        //    var other = repository.GetByAttribute(keyValue, t => (TKey?)t?.GetType().GetProperty(_keyName)?.GetValue(t), false);
        //    if (other.Any(t => ((TProp?)t?.GetType().GetProperty(_attributeName)?.GetValue(t))?.Equals(propertyValue) == true))
        //        return new ValidationResult($"{_attributeName} must be unique.");
        //    return ValidationResult.Success;
        //}
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        //=> IsValid((T?)value, validationContext);
        {
            var studentRepository = (IBaseRepository<Student>)validationContext.GetService(typeof(IBaseRepository<Student>))!;
            var std = validationContext.ObjectInstance as Student;
            if (std == null || string.IsNullOrWhiteSpace(std.Email))
                return ValidationResult.Success;
            if (studentRepository.GetByAttribute(std.Ssn, s => s.Ssn, false).Any(s => s.Email == std.Email))
                return new ValidationResult("Email must be unique.");
            return ValidationResult.Success;
        }
    }
}
