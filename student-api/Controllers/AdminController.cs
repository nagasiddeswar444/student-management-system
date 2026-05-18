using Microsoft.AspNetCore.Mvc;
using StudentAPI.DTObjects;
using StudentAPI.Services.Interfaces;

namespace StudentAPI.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public AdminController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingStudents()
        {
            var students = await _studentService.GetStudentsByStatusAsync("Pending");
            return Ok(students);
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedStudents()
        {
            var students = await _studentService.GetStudentsByStatusAsync("Approved");
            return Ok(students);
        }

        [HttpGet("rejected")]
        public async Task<IActionResult> GetRejectedStudents()
        {
            var students = await _studentService.GetStudentsByStatusAsync("Rejected");
            return Ok(students);
        }

        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveStudent(int id)
        {
            var result = await _studentService.ApproveStudentAsync(id);

            if (result.Contains("not found") || result.Contains("not allowed"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectStudent(int id, AdminDecisionDto dto)
        {
            var result = await _studentService.RejectStudentAsync(id, dto.RejectionReason);

            if (result.Contains("not found") || result.Contains("not allowed"))
                return BadRequest(result);

            return Ok(result);
        }
    }
}