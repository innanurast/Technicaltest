namespace SalesApp.Model
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        //public int  Qty { get; set; }

        public Category Categories { get; set; }

        public ICollection<SaleProduct> SalesProducts { get; set; }
    }
}
