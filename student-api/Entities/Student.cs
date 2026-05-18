using System;
using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string FatherName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(150)]
        public string CollegeName { get; set; }

        [Required]
        [MaxLength(50)]
        public string YearOfStudy { get; set; }

        [Required]
        [MaxLength(100)]
        public string Course { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public string PasswordHash { get; set; }

        public string ActivationToken { get; set; }

        public DateTime? TokenExpiry { get; set; }

        public bool IsActivated { get; set; } = false;

        public string RejectionReason { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}