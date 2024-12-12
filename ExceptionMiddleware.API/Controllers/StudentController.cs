using ExceptionMiddleware.API.Data;
using ExceptionMiddleware.API.Dtos;
using ExceptionMiddleware.API.Exceptions;
using ExceptionMiddleware.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExceptionMiddleware.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ExceptionMiddlewareDbContext _context;

        public StudentController(ExceptionMiddlewareDbContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.Include(s => s.Class).ToListAsync();
        }
        // POST: api/Class
        [HttpPost]
        public async Task<ActionResult<ClassDto>> PostStudent(StudentDto studentEntity)
        {
            var item = new Student()
            {
                Name = studentEntity.Name,
                ClassId = studentEntity.ClassId,
            };
            _context.Students.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.Include(s => s.Class).FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                throw new EntityNotFoundException("");
            }

            return student;
        }

        

       
        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                throw new EntityNotFoundException("");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

      
    }
}