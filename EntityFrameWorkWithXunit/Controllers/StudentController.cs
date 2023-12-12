using EntityFrameWorkWithXunit.Domain;
using EntityFrameWorkWithXunit.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWorkWithXunit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudentsAsync()
        {
            var result = await _studentService.GetStudentsAsync();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<Student>> GetStudentsByIdAsync(int id)
        {
            var result = await _studentService.GetStudentByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> SaveStudentAsync(Student student)
        {
            await _studentService.SaveStudentAsync(student);
            return Ok();
        }
    }
}
