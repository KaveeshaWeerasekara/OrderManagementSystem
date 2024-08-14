using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProductService.Models
{
    [Table("orderProduct")]
    public class OrderProductModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("order_product_id")]
        public long id { get; set; }
        //  public int OrderProductID { get; set; }
        [Column("order_id")]
        public long OrderID { get; set; }
        [Column("product_id")]
        public long ProductID { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
