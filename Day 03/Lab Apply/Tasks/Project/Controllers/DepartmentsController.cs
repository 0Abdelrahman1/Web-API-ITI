using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Contexts;
using Project.DTOs;
using Project.Filters;
using Project.Models;
using Project.Repositories;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[HandleExceptionFilter]
    public class DepartmentsController : ControllerBase
    {
        private readonly IBaseRepository<Department> _departmentsRepository;

        public DepartmentsController(IBaseRepository<Department> departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        [HttpGet]
        //[HandleExceptionFilter]
        [ResultFilter]
        public IActionResult GetAll()
        {
            var departments = _departmentsRepository.GetAll(d => d.Students);
            List<SubDepartmentWithStudentsDTO> departmentsWithStudentsInfo = new();

            foreach (var department in departments)
            {
                departmentsWithStudentsInfo.Add(
                    new SubDepartmentWithStudentsDTO(
                        department.Name,
                        department.Students.Select(s => s.Name).ToList(),
                        department.Students.Count,
                        department.Students.Count > 1 ? "overload" : "normal")
                    );
            }
            return Ok(departmentsWithStudentsInfo);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var department = _departmentsRepository.GetByKey(id, d => d.Id, d => d.Students);
            if (department == null) return NotFound();

            var data = new DepartmentWithStudentsDTO(
                department.Id,
                department.Name,
                department.Location,
                department.PhoneNumber,
                department.Manager,
                department.Students.Select(s => new StudentWithoutDepartmentDTO(
                    s.Name, s.Ssn, s.DateOfBirth, s.Age, s.Email, s.Address, s.Image, s.Level)).ToList()
                );

            return Ok(new { data = data, msg = "Department found successfully" });
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var departments = _departmentsRepository.GetByAttribute(name, d => d.Name, includes: d => d.Students);
            if (departments.Count == 0) return NotFound();

            var data = departments.Select(department => new DepartmentWithStudentsDTO(
                department.Id,
                department.Name,
                department.Location,
                department.PhoneNumber,
                department.Manager,
                department.Students.Select(s => new StudentWithoutDepartmentDTO(
                    s.Name, s.Ssn, s.DateOfBirth, s.Age, s.Email, s.Address, s.Image, s.Level)).ToList()
                )).ToList();

            return Ok(new { data = data, msg = "Departments found successfully" });
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (department == null) return NotFound();
            _departmentsRepository.Add(department);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, new { data = department, msg = "Department created successfully" });
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(Department department, int id)
        {
            if (department == null) return NotFound();
            department.Id = id;
            _departmentsRepository.Update(department);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var department = _departmentsRepository.GetByKey(id, d => d.Id);
            if (department == null) return NotFound();
            _departmentsRepository.Delete(id);
            return Ok(new { data = department, msg = "Department deleted successfully" });
        }
    }
}
