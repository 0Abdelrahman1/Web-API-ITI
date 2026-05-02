using System.ComponentModel.DataAnnotations;

namespace Project.Validators
{
    public class NonFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var date = value as DateTime?;
            return date <= DateTime.Now;
        }
    }
}
