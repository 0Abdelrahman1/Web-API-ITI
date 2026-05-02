using Project.Validators;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Student
    {
        [Key]
        public ulong Ssn { get; set; }
        
        [MinLength(5)]
        [MaxLength(12)]
        [RegularExpression(@"^[a-z A-Z]+$")]
        public string Name { get; set; }

        [UniqueEmail]
        public string Email { get; set; } = string.Empty;

        [Range(18, 20)]
        public byte Age { get; set; }

        [CompareToAge(ErrorMessage = "Date of birth does not match age.")]
        [NonFuture(ErrorMessage = "Date of birth cannot be in the future.")]
        public DateTime? DateOfBirth { get; set; } = DateTime.Now.AddYears(-18);
        public string Address { get; set; }
        public string Image { get; set; }
        public byte Level { get; set; }
    }
}
