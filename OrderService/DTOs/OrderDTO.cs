using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs
{
    public class OrderDTO
    {
        [Required]
        public long id { get; set; }
       
        [Required]
        public long userID { get; set; }
        [Required]
        public DateTime orderPlaceDate { get; set; }

    }
}
