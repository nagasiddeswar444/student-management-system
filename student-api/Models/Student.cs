using System;

namespace StudentAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string FatherName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string CollegeName { get; set; } = "";
        public string YearOfStudy { get; set; } = "";
        public string Course { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string Status { get; set; } = "";
    }
}