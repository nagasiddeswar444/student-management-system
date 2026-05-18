using System;
using System.ComponentModel.DataAnnotations;

namespace StudentAPI.DTObjects
{
    public class StudentRegistrationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string CollegeName { get; set; }

        [Required]
        public string YearOfStudy { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}