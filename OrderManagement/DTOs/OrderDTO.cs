using System.ComponentModel.DataAnnotations;

namespace OrderManagement.DTOs
{
    public class OrderDTO
    {
        [Required]
        public long id { get; set; }
       // [Required] 
       // public int OrderID { get;set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public DateTime OrderPlaceDate { get; set; }
        

    }
}
