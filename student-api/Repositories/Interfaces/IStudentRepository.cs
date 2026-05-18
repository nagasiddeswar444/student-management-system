using StudentAPI.Entities;

namespace StudentAPI.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student student);

        Task<Student?> GetStudentByEmailAsync(string email);

        Task<Student?> GetStudentByPhoneAsync(string phone);

        Task<Student?> GetStudentByIdAsync(int id);

        Task<Student?> GetStudentByTokenAsync(string token);

        Task<List<Student>> GetStudentsByStatusAsync(string status);

        Task<List<Student>> GetAllStudentsAsync();

        Task UpdateStudentAsync(Student student);
    }
}