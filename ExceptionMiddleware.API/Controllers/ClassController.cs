using ExceptionMiddleware.API.Data;
using ExceptionMiddleware.API.Dtos;
using ExceptionMiddleware.API.Exceptions;
using ExceptionMiddleware.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExceptionMiddleware.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ExceptionMiddlewareDbContext _context;

        public ClassController(ExceptionMiddlewareDbContext context)
        {
            _context = context;
        }

        // GET: api/Class
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Class/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);

            if (classEntity == null)
            {
                throw new EntityNotFoundException("");
            }

            return classEntity;
        }

        // POST: api/Class
        [HttpPost]
        public async Task<ActionResult<ClassDto>> PostClass(ClassDto classEntity)
        {
            var item = new Class()
            {
                Name = classEntity.Name,
            };
            _context.Classes.Add(item);
            await _context.SaveChangesAsync();
            return Ok (item);
        }


        // DELETE: api/Class/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity == null)
            {
                throw new EntityNotFoundException("");
            }

            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
