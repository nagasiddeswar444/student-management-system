using StudentAPI.DTObjects;

namespace StudentAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task<string> RegisterStudentAsync(StudentRegistrationDto dto);
        Task<StudentResponseDto> GetStudentByEmailAsync(string email);
        Task<string> CreatePasswordAsync(CreatePasswordDto dto);
        Task<string> LoginAsync(StudentLoginDto dto);

        Task<List<StudentResponseDto>> GetStudentsByStatusAsync(string status);

        Task<List<StudentResponseDto>> GetAllStudentsAsync();

        Task<StudentResponseDto> GetStudentByIdAsync(int id);

        Task<string> ApproveStudentAsync(int id);

        Task<string> RejectStudentAsync(int id, string reason);
    }
}