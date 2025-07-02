using LecDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LecDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StuAPIController : ControllerBase
    {
        private readonly LecDemoContext context;

        public StuAPIController(LecDemoContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetStudent()
        {
            var data = context.Students.ToList();

            return Ok(data);
        }
        [HttpGet("{StudentId}")]
        public IActionResult GetStudentById(int StudentId)     
        {
            var student = context.Students.Find(StudentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudents(Student std) 
        {
            context.Students.AddAsync(std);
            context.SaveChanges();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public IActionResult EditStudents(int id, Student std)
        {
            if (id != std.StudentId)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            context.SaveChanges();
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id) 
        {
            var data = context.Students.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            context.Students.Remove(data);
            context.SaveChanges();
            return Ok(data);
        }
    }
}
