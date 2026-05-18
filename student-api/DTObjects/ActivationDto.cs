using System.ComponentModel.DataAnnotations;

namespace StudentAPI.DTObjects
{
    public class ActivationDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}