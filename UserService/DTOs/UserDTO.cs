using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs
{
    public class UserDTO
    {
        [Required]
        public long id { get; set; }
        
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
