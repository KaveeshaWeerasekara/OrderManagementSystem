using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Models
{
    [Table("order")]
    public class OrderModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("order_id")]
        public long id { get; set; }

        // public int OrderID { get; set; }

        [Column("user_id")]
        public long UserID { get; set; }
        [Column("orderplace_date")]
        public DateTime OrderPlaceDate { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
