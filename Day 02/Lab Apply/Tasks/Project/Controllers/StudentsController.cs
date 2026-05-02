using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Contexts;
using Project.Filters;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[HandleExceptionFilter]
    public class StudentsController : ControllerBase
    {
        private readonly StudentManagementDB _context;

        public StudentsController(StudentManagementDB context)
        {
            _context = context;
        }

        [HttpGet]
        //[HandleExceptionFilter]
        [ResultFilter]
        public IActionResult GetAll()
        {
            var students = _context.Students.ToList();
            if (students.Count == 0)
                return NotFound();
            return Ok(students);
        }

        [HttpGet("{ssn:long}")]
        public IActionResult GetById(ulong ssn)
        {
            var student = _context.Students.FirstOrDefault(s => s.Ssn == ssn);
            if (student == null)
                return NotFound();
            return Ok(new { data = student, msg = "Student found successfully" });
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var students = _context.Students.Where(s => s.Name.ToLower() == name.ToLower()).ToList();
            if (students.Count == 0)
                return NotFound();
            return Ok(new { data = students, msg = "Students found successfully" });
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student == null) return NotFound();
            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { ssn = student.Ssn }, new { data = student, msg = "Student created successfully" });
        }

        [HttpPut("{ssn:long}")]
        public IActionResult Edit(Student student, ulong ssn)
        {
            var std = _context.Students.FirstOrDefault(s => s.Ssn == (ulong)ssn);
            if (student == null) return NotFound();
            std.Name = student.Name;
            std.Address = student.Address;
            std.Email = student.Email;
            std.Age = student.Age;
            std.Image = student.Image;
            std.Level = student.Level;
            std.DateOfBirth = student.DateOfBirth;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{ssn:long}")]
        public IActionResult Delete(ulong ssn)
        {
            var student = _context.Students.FirstOrDefault(s => s.Ssn == ssn);
            if (student == null) return NotFound();
            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok(new { data = student, msg = "Student deleted successfully" });
        }
    }
}