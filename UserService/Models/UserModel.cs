using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    [Table("user")]
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public long id { get; set; }
        [Column("first_name")]
        public string FName { get; set; }
        [Column("last_name")]
        public string LName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
