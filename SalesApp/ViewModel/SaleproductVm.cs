using SalesApp.Model;

namespace SalesApp.ViewModel
{
    public class SaleproductVm
    {
        public string product_id { get; set; }
        public DateTime dateSale { get; set; }
        public int qty { get; set; }
        public float totalPrice { get; set; }
    }

    public class SaleproductAndProductVm
    {
        public string saleProductId { get; set; }
        public string product_id { get; set; }
        public DateTime dateSale { get; set; }
        public int qty { get; set; }
        public float totalPrice { get; set; }
        public string productName { get; set; }

        public float price { get; set; }

        public string categoryName { get; set; }

        public Product product { get; set; }
    }

}
