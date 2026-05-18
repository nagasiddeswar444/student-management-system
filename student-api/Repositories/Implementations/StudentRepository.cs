using Microsoft.Data.SqlClient;
using StudentAPI.Data;
using StudentAPI.Entities;
using StudentAPI.Repositories.Interfaces;
using System.Data;

namespace StudentAPI.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("AddStudent", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@FatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Phone", student.Phone);
            cmd.Parameters.AddWithValue("@CollegeName", student.CollegeName);
            cmd.Parameters.AddWithValue("@YearOfStudy", student.YearOfStudy);
            cmd.Parameters.AddWithValue("@Course", student.Course);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@Status", student.Status);
            cmd.Parameters.AddWithValue("@PasswordHash", student.PasswordHash);
            cmd.Parameters.AddWithValue("@ActivationToken", student.ActivationToken);
            cmd.Parameters.AddWithValue("@IsActivated", student.IsActivated);
            cmd.Parameters.AddWithValue("@RejectionReason", student.RejectionReason);
            cmd.Parameters.AddWithValue("@CreatedAt", student.CreatedAt);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return student;
        }

        public async Task<Student?> GetStudentByEmailAsync(string email)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("CheckStudentEmail", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);

            await con.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapStudent(reader);
            }

            return null;
        }

        public async Task<Student?> GetStudentByPhoneAsync(string phone)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("CheckStudentPhone", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Phone", phone);

            await con.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapStudent(reader);
            }

            return null;
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("GetStudentById", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            await con.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapStudent(reader);
            }

            return null;
        }

        public async Task<Student?> GetStudentByTokenAsync(string token)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("GetStudentByToken", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Token", token);

            await con.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapStudent(reader);
            }

            return null;
        }

        public async Task<List<Student>> GetStudentsByStatusAsync(string status)
        {
            List<Student> students = new List<Student>();

            using SqlConnection con = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("GetStudentsByStatus", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", status);

            await con.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                students.Add(MapStudent(reader));
            }

            return students;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            List<Student> students = new List<Student>();

            using SqlConnection con = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("GetAllStudents", con);

            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                students.Add(MapStudent(reader));
            }

            return students;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("UpdateStudent", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", student.Id);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@FatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Phone", student.Phone);
            cmd.Parameters.AddWithValue("@CollegeName", student.CollegeName);
            cmd.Parameters.AddWithValue("@YearOfStudy", student.YearOfStudy);
            cmd.Parameters.AddWithValue("@Course", student.Course);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@Status", student.Status);
            cmd.Parameters.AddWithValue("@PasswordHash", student.PasswordHash);
            cmd.Parameters.AddWithValue("@ActivationToken", student.ActivationToken);
            cmd.Parameters.AddWithValue("@IsActivated", student.IsActivated);
            cmd.Parameters.AddWithValue("@RejectionReason", student.RejectionReason);

            if (student.ApprovedDate == null)
                cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ApprovedDate", student.ApprovedDate);

            await con.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        private Student MapStudent(SqlDataReader reader)
        {
            return new Student
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                FatherName = reader["FatherName"].ToString(),
                Email = reader["Email"].ToString(),
                Phone = reader["Phone"].ToString(),
                CollegeName = reader["CollegeName"].ToString(),
                YearOfStudy = reader["YearOfStudy"].ToString(),
                Course = reader["Course"].ToString(),
                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                Status = reader["Status"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                ActivationToken = reader["ActivationToken"].ToString(),
                IsActivated = Convert.ToBoolean(reader["IsActivated"]),
                RejectionReason = reader["RejectionReason"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                ApprovedDate = reader["ApprovedDate"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["ApprovedDate"])
            };
        }
    }
}