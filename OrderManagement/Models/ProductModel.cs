using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Models
{
    [Table("product")]
    public class ProductModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public long id { get; set; }
        [Column("product_name")]
        public string ProductName { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("category")]
        public string Category { get; set; }
        [Column("stored_quantity")]
        public int StoredQuantity { get; set; }
        
    }
}
