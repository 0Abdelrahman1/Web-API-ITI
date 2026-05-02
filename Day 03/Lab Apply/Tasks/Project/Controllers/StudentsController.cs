using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Contexts;
using Project.Filters;
using Project.Models;
using Project.Repositories;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[HandleExceptionFilter]
    public class StudentsController : ControllerBase
    {
        private readonly IBaseRepository<Student> _studentsRepository;

        public StudentsController(IBaseRepository<Student> studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        //[HandleExceptionFilter]
        [ResultFilter]
        public IActionResult GetAll()
        {
            return Ok(_studentsRepository.GetAll());
        }

        [HttpGet("{ssn:int}")]
        public IActionResult GetById(int ssn)
        {
            var student = _studentsRepository.GetByKey(ssn, s => s.Ssn);
            if (student == null)
                return NotFound();
            return Ok(new { data = student, msg = "Student found successfully" });
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var students = _studentsRepository.GetByAttribute(name, s => s.Name);
            if (students.Count == 0)
                return NotFound();
            return Ok(new { data = students, msg = "Students found successfully" });
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student == null) return NotFound();
            _studentsRepository.Add(student);
            return CreatedAtAction(nameof(GetById), new { ssn = student.Ssn }, new { data = student, msg = "Student created successfully" });
        }

        [HttpPut("{ssn:int}")]
        public IActionResult Edit(Student student, int ssn)
        {
            if (student == null) return NotFound();
            student.Ssn = ssn;
            _studentsRepository.Update(student);
            return NoContent();
        }

        [HttpDelete("{ssn:int}")]
        public IActionResult Delete(int ssn)
        {
            var student = _studentsRepository.GetByKey(ssn, s => s.Ssn);
            if (student == null) return NotFound();
            _studentsRepository.Delete(student);
            return Ok(new { data = student, msg = "Student deleted successfully" });
        }
    }
}