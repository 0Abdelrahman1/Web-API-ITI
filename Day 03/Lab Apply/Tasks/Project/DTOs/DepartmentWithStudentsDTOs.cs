using Project.Models;
using Project.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DTOs
{
    public record SubDepartmentWithStudentsDTO(string DepartmentName, List<string> Students, int StudentsCount, string Msg);
    public record DepartmentWithStudentsDTO(int Id, string Name, string Location, string PhoneNumber, string Manager, List<StudentWithoutDepartmentDTO> Students);

    public record StudentWithoutDepartmentDTO(string Name, int Ssn, DateTime? DateOfBirth, byte Age,
            string Email, string Address, string Image, byte Level);
}
