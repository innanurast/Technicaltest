using Microsoft.EntityFrameworkCore;
using SalesApp.Context;
using SalesApp.Model;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext context;

        public ProductRepository(MyContext context)
        {
            this.context = context; //this.context sama seperti perintah _context
        }

        public int Delete(string ProductId)
        {
            var data = context.Products.Find(ProductId);
            context.Remove(data);
            var save = context.SaveChanges();
            return save;
        }

        public ProductAndCategoryVm Get(string ProductId)
        {
            var data = GetProduct().FirstOrDefault(p => p.ProductId == ProductId);
            return data;
        }

        public IEnumerable<ProductAndCategoryVm> Get()
        {
            return GetProduct().ToList();
        }

        public int Insert(ProductVm productVm)
        {
            string Idnew = ProductID();

            //data product
            Product dataProduct = new Product();
            dataProduct.ProductId = Idnew;
            dataProduct.Name = productVm.Name;
            dataProduct.Description = productVm.Description;
            dataProduct.Price = productVm.Price;
            dataProduct.CategoryId = productVm.CategoryId;
            context.Products.Add(dataProduct);
            var result_product = context.SaveChanges();
            return result_product;
        }

        public string ProductID()
        {
            string date = DateTime.Now.ToString("ddMMyy"); //format tgl sekarang
            string uniqueID = "";

            var cekLastData = context.Products.OrderBy(data => data.ProductId).LastOrDefault(); //Cek data terakhir
            if (cekLastData == null) // data null adalah nilai pada suatu kolom yang berarti tidak mempunyai nilai.
            {
                uniqueID = date + "001";
            }
            else
            {
                var LastData = cekLastData.ProductId;            //dari cek data terakhir id maka akan di
                string lastThree = LastData.Substring(LastData.Length - 3);       //substring menspesifikan karakter

                int kode = int.Parse(lastThree) + 1;        //increement
                uniqueID = date + kode.ToString("000"); // membuat kode unik id dengan format tgl bulan tahun kemudian tambah dengan urutan
                                                         //  mengembalikan nilai string yang merupakan representasi obyek
            }
            return uniqueID;
        }

        public int Update(string ProductId, ProductVm productVm)
        {
            var data = context.Products.Find(ProductId);
            data.Name = productVm.Name;
            data.Description = productVm.Description;
            data.Price = productVm.Price;
            data.CategoryId = productVm.CategoryId;

            var result = context.SaveChanges();
            return result;

        }

        public IEnumerable<ProductAndCategoryVm> GetProduct()
        {
            var data = context.Products
                .Join(context.Categories,
                p => p.CategoryId,
                c => c.Id,
                (p, c) => new ProductAndCategoryVm
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    category = new Category
                    {
                        Id = c.Id,
                        Name = c.Name
                    }

                });
            return data;
        }


        //public IEnumerable<ProductAndCategoryVm> GetProducts(int start, int length, string searchValue)
        //{
        //    // Lakukan logika untuk mengambil data karyawan dari database.
        //    // Misalnya, Anda bisa menggunakan Entity Framework untuk mengambil data.
        //    // Contoh:

        //    var query = context.Products.Include(p => p.Categories).AsQueryable();

        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        query = query.Where(p => p.Name.Contains(searchValue) || p.ProductId.Contains(searchValue));
        //    }

        //    var products = query
        //        .Skip(start)
        //        .Take(length)
        //        .ToList()
        //        .Select((a, index) => new ProductAndCategoryVm
        //        {
        //            Name = a.Name,
        //            Description = a.Description,
        //            Price = a.Price,
        //            category = new Category
        //            {
        //                Id = a.Categories.Id, // Akses properti Department
        //                Name = a.Categories.Name // Akses properti Department
        //            }
        //        });

        //    return products;
        //}


        //public int GetTotalDisplayRecords(string searchValue)
        //{
        //    var query = context.Products.AsQueryable();

        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        query = query.Where(e => e.Name.Contains(searchValue) || e.ProductId.Contains(searchValue));
        //    }

        //    return query.Count();
        //}

        //public int GetTotalProductCount(string searchValue)
        //{

        //    return context.Products.Count();
        //}

        //public int GetTotalRecord()
        //{
        //    return context.Products.Count();
        //}

        public bool CheckIDExist(string ProductId)
        {
            var checkID = context.Products.AsNoTracking().FirstOrDefault(e => e.ProductId == ProductId);
            if (checkID == null)
            {
                return false;
            }
            return true;
        }

        public bool HasSaleProductReferences(string productId)
        {
            // Periksa apakah ada relasi di tabel SaleProduct berdasarkan ID produk
            return context.SaleProducts.Any(sp => sp.product_id == productId);
        }

    }
}
