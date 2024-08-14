using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs.Requests
{
    public class CreateUserRequestDTOs
    {
        [Required]
        public string? FName { get; set; }
        [Required]
        public string? LName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
    }
}
