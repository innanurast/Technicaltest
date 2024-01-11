using System.ComponentModel.DataAnnotations;

namespace SalesApp.Model
{
    public class SaleProduct
    {
        [Key]
        public string saleProductId { get; set; }
        public string product_id { get; set; }
        public DateTime dateSale { get; set; }
        public int qty { get; set; }
        public float totalPrice { get; set; }

        public Product product { get; set; }

    }
}
