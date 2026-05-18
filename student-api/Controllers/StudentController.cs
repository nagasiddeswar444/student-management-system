using Microsoft.AspNetCore.Mvc;
using StudentAPI.DTObjects;
using StudentAPI.Services.Interfaces;

namespace StudentAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent(StudentRegistrationDto dto)
        {
            var result = await _studentService.RegisterStudentAsync(dto);

            if (result.Contains("already"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("status/{email}")]
        public async Task<IActionResult> GetStatus(string email)
        {
            var student = await _studentService.GetStudentByEmailAsync(email);

            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        [HttpPost("create-password")]
        public async Task<IActionResult> CreatePassword(CreatePasswordDto dto)
        {
            var result = await _studentService.CreatePasswordAsync(dto);

            if (result == "Invalid token.")
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(StudentLoginDto dto)
        {
            var result = await _studentService.LoginAsync(dto);

            if (result != "Login successful.")
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("details/{email}")]
        public async Task<IActionResult> GetDetails(string email)
        {
            var student = await _studentService.GetStudentByEmailAsync(email);

            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }
    }
}