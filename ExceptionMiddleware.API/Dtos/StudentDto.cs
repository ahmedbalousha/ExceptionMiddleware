using System.Security.Claims;

namespace ExceptionMiddleware.API.Dtos
{
    public class StudentDto
    {
        public string Name { get; set; }
        public int ClassId { get; set; }

    }
}
