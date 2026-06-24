using crudApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly MyDbContext _dbcontext;
        public StudentController(MyDbContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var students = await _dbcontext.Students.ToListAsync();
            if (students != null)
            {

                return Ok(new
                {
                    success = true,
                    message = "Students retrieved successfully",
                    data = students
                });
            }
            return Ok("No Student Found");
        }

        [HttpPost]

        public async Task<IActionResult> createStudent(Student student)
        {
           var studentData =  await _dbcontext.Students.AddAsync(student);
            await _dbcontext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Student created successfully",
                data = studentData.ToString(),
                
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteStudent(int id)
        {
            var student = await _dbcontext.Students.FindAsync(id);
            if(student != null)
            {
             _dbcontext.Students.Remove(student);
            await _dbcontext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Student deleted successfully",
                data = student

            });
            }
            return NotFound();
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _dbcontext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                success = true,
                data = student
            });
        }

        [HttpPut("update")]

        public async Task<IActionResult> update(Student student)
        {
            var studentData = await _dbcontext.Students.FindAsync(student.Id);
            if(studentData == null)
            {
                return NotFound();
            }
            studentData.Name = student.Name;
            studentData.Email = student.Email;
            await _dbcontext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Student updated successfully",
                data = studentData,

            });
        }
    }
}

