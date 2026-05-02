using Project.Validators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Project.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [UniqueName]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }
        
        [RegularExpression(@"^\+\d{1,3}\d{10}$", ErrorMessage = "Phone number must include a country code (e.g., +20) and 10 digits.")]
        public string PhoneNumber { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        public string Manager { get; set; }
        public virtual ICollection<Student>? Students { get; set; } = new List<Student>();

    }
}