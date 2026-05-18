using StudentAPI.DTObjects;
using StudentAPI.Entities;
using StudentAPI.Repositories.Interfaces;
using StudentAPI.Services.Interfaces;
using StudentAPI.Services.Email;

namespace StudentAPI.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public StudentService(
            IStudentRepository studentRepository,
            IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public async Task<string> RegisterStudentAsync(StudentRegistrationDto dto)
        {
            var existingEmail = await _studentRepository.GetStudentByEmailAsync(dto.Email);

            if (existingEmail != null)
                return "Email already registered.";

            var existingPhone = await _studentRepository.GetStudentByPhoneAsync(dto.Phone);

            if (existingPhone != null)
                return "Phone number already registered.";

            var student = new Student
            {
                Name = dto.Name,
                FatherName = dto.FatherName,
                Email = dto.Email,
                Phone = dto.Phone,
                CollegeName = dto.CollegeName,
                YearOfStudy = dto.YearOfStudy,
                Course = dto.Course,
                DateOfBirth = dto.DateOfBirth,
                Status = "Pending",
                ActivationToken = Guid.NewGuid().ToString(),
                PasswordHash = "TempPassword123",
                CreatedAt = DateTime.UtcNow,
                IsActivated = false,
                RejectionReason = "",
                ApprovedDate = null,
                TokenExpiry = null
            };

            await _studentRepository.AddStudentAsync(student);

            return "Application submitted successfully.";
        }

        public async Task<StudentResponseDto> GetStudentByEmailAsync(string email)
        {
            var student = await _studentRepository.GetStudentByEmailAsync(email);

            if (student == null)
                return null;

            return new StudentResponseDto
            {
                Id = student.Id,
                Name = student.Name,
                FatherName = student.FatherName,
                Email = student.Email,
                Phone = student.Phone,
                CollegeName = student.CollegeName,
                YearOfStudy = student.YearOfStudy,
                Course = student.Course,
                Status = student.Status,
                RejectionReason = student.RejectionReason
            };
        }

        public async Task<string> CreatePasswordAsync(CreatePasswordDto dto)
        {
            var targetStudent =
                await _studentRepository.GetStudentByTokenAsync(dto.Token);

            if (targetStudent == null)
                return "Invalid token.";

            targetStudent.PasswordHash = dto.Password;
            targetStudent.IsActivated = true;
            targetStudent.ActivationToken = Guid.NewGuid().ToString();

            await _studentRepository.UpdateStudentAsync(targetStudent);

            return "Password created successfully.";
        }

        public async Task<string> LoginAsync(StudentLoginDto dto)
        {
            var student = await _studentRepository.GetStudentByEmailAsync(dto.Email);

            if (student == null)
                return "Student not found.";

            if (!student.IsActivated)
                return "Account not activated.";

            if (student.PasswordHash != dto.Password)
                return "Invalid password.";

            return "Login successful.";
        }

        public async Task<List<StudentResponseDto>> GetStudentsByStatusAsync(string status)
        {
            var students = await _studentRepository.GetStudentsByStatusAsync(status);

            return students.Select(s => new StudentResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                FatherName = s.FatherName,
                Email = s.Email,
                Phone = s.Phone,
                CollegeName = s.CollegeName,
                YearOfStudy = s.YearOfStudy,
                Course = s.Course,
                Status = s.Status,
                RejectionReason = s.RejectionReason
            }).ToList();
        }

        public async Task<List<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();

            return students.Select(s => new StudentResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                FatherName = s.FatherName,
                Email = s.Email,
                Phone = s.Phone,
                CollegeName = s.CollegeName,
                YearOfStudy = s.YearOfStudy,
                Course = s.Course,
                Status = s.Status,
                RejectionReason = s.RejectionReason
            }).ToList();
        }

        public async Task<StudentResponseDto> GetStudentByIdAsync(int id)
        {
            var s = await _studentRepository.GetStudentByIdAsync(id);

            if (s == null)
                return null;

            return new StudentResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                FatherName = s.FatherName,
                Email = s.Email,
                Phone = s.Phone,
                CollegeName = s.CollegeName,
                YearOfStudy = s.YearOfStudy,
                Course = s.Course,
                Status = s.Status,
                RejectionReason = s.RejectionReason
            };
        }

        public async Task<string> ApproveStudentAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);

            if (student == null)
                return "Student not found.";

            if (student.Status != "Pending")
                return "Action not allowed.";

            student.Status = "Approved";
            student.ApprovedDate = DateTime.UtcNow;

            if (string.IsNullOrEmpty(student.ActivationToken))
            {
                student.ActivationToken = Guid.NewGuid().ToString();
            }

            await _studentRepository.UpdateStudentAsync(student);

            string activationLink =
                $"http://localhost:3000/create-password/{student.ActivationToken}";

            await _emailService.SendEmailAsync(
                student.Email,
                "Application Approved",
                $"Congratulations! Your application is approved.<br/><a href='{activationLink}'>Create Password</a>"
            );

            return "Student approved successfully and email sent.";
        }

        public async Task<string> RejectStudentAsync(int id, string reason)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);

            if (student == null)
                return "Student not found.";

            if (student.Status != "Pending")
                return "Action not allowed.";

            student.Status = "Rejected";
            student.RejectionReason = reason;

            await _studentRepository.UpdateStudentAsync(student);

            await _emailService.SendEmailAsync(
                student.Email,
                "Application Rejected",
                $"We are sorry. Your application has been rejected.<br/><b>Reason:</b> {reason}"
            );

            return "Student rejected successfully and email sent.";
        }
    }
}