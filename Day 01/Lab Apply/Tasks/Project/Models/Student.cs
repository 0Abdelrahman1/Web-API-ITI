using System.ComponentModel.DataAnnotations;

namespace Task1.Models
{
    public class Student
    {
        [Key]
        public ulong Ssn { get; set; }
        public string Name { get; set; }
        public byte Age { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public byte Level { get; set; }
    }
}
