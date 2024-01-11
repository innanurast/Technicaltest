using SalesApp.Model;

namespace SalesApp.ViewModel
{
    public class ProductVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        //public int Qty { get; set; }
    }

    public class ProductAndCategoryVm
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }

        public Category category { get; set; }
    }
}
