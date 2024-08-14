using System.ComponentModel.DataAnnotations;

namespace OrderManagement.DTOs.Requests
{
    public class CreateUserRequest
    {
       // [Required]
        //public int UserID { get; set; }
        [Required]
        public string? FName { get; set; }
        [Required]
        public string? LName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string?  PhoneNumber { get; set; }
    }
}
