using System.ComponentModel.DataAnnotations;

namespace OrderManagement.DTOs
{
    public class OrderProductDTO
    {
        [Required]
        public long id { get; set; }
       // [Required] 
       // public int OrderProductID { get;set; }
        [Required]
        public long OrderID { get; set; }
        [Required]
        public long ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        

    }
}
