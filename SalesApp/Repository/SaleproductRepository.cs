using Microsoft.EntityFrameworkCore;
using SalesApp.Context;
using SalesApp.Model;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Repository
{
    public class SaleproductRepository : ISaleproductRepository
    {
        private readonly MyContext context;

        public SaleproductRepository(MyContext context)
        {
            this.context = context; //this.context sama seperti perintah _context
        }
        public int Delete(string saleProductId)
        {
            var data = context.SaleProducts.Find(saleProductId);
            context.Remove(data);
            var save = context.SaveChanges();
            return save;
        }

        public IEnumerable<SaleproductAndProductVm> Get()
        {
            return GetSaleProduct().ToList();
        }

        public SaleproductAndProductVm Get(string saleProductId)
        {
            var data = GetSaleProduct().FirstOrDefault(p => p.saleProductId == saleProductId);
            return data;
        }

        public int Insert(SaleproductVm saleproduct)
        {
            string Idnew = SaleProductID();

            //data product
            SaleProduct dataSaleproduct = new SaleProduct();
            dataSaleproduct.saleProductId = Idnew;
            dataSaleproduct.dateSale = saleproduct.dateSale;
            dataSaleproduct.qty = saleproduct.qty;
            dataSaleproduct.product_id = saleproduct.product_id;

            var product = context.Products.Find(saleproduct.product_id);

            if (product != null)
            {
                // Hitung total harga menggunakan harga produk dari data produk
                dataSaleproduct.totalPrice = saleproduct.qty * product.Price;

                dataSaleproduct.product_id = saleproduct.product_id;

                context.SaleProducts.Add(dataSaleproduct);
                var result_product = context.SaveChanges();
                return result_product;
            }

            // Handle ketika produk tidak ditemukan
            return -1;
        }

        public string SaleProductID()
        {
            string date = DateTime.Now.ToString("ddMMyy"); //format tgl sekarang
            string id = "";

            var cekLastData = context.SaleProducts.OrderBy(data => data.saleProductId).LastOrDefault(); //Cek data terakhir
            if (cekLastData == null) // data null adalah nilai pada suatu kolom yang berarti tidak mempunyai nilai.
            {
                id = date + "SP"+ "001";
            }
            else
            {
                var LastData = cekLastData.saleProductId;            //dari cek data terakhir id maka akan di
                string lastThree = LastData.Substring(LastData.Length - 3);       //substring menspesifikan karakter

                int kode = int.Parse(lastThree) + 1;        //increement
                id = date +"SP" + kode.ToString("000"); // membuat kode unik id dengan format tgl bulan tahun kemudian tambah dengan urutan
                                                        //  mengembalikan nilai string yang merupakan representasi obyek
            }
            return id;
        }


        public int Update(string saleProductId, SaleproductVm saleproduct)
        {
            var data = context.SaleProducts.Find(saleProductId);
            data.dateSale = saleproduct.dateSale;
            data.qty = saleproduct.qty;

            var product = context.Products.Find(saleproduct.product_id);

            // Periksa apakah produk ditemukan
            if (product != null)
            {
                // Hitung total harga menggunakan harga produk dari data produk
                data.totalPrice = saleproduct.qty * product.Price;

                data.product_id = saleproduct.product_id;

                var result = context.SaveChanges();
                return result;
            }

            // Handle ketika produk tidak ditemukan
            return -1; // Atau sesuaikan dengan tindakan yang sesuai
        }

        public IEnumerable<SaleproductAndProductVm> GetSaleProduct()
        {
            var data = context.SaleProducts
                .Join(context.Products,
                s => s.product_id,
                p => p.ProductId,
                (s, p) => new {s, p}
                )
                .Join(context.Categories,
                p_cat => p_cat.p.CategoryId,
                cat => cat.Id,
                (p_cat, cat) => new SaleproductAndProductVm
                {
                    saleProductId = p_cat.s.saleProductId,
                    dateSale = p_cat.s.dateSale,
                    qty = p_cat.s.qty,
                    totalPrice = p_cat.s.totalPrice,
                    product_id = p_cat.s.product_id,
                    productName = p_cat.p.Name,
                    price = p_cat.p.Price,
                    categoryName = cat.Name,
                    product = new Product
                    {
                        ProductId = p_cat.p.ProductId,
                        Name = p_cat.p.Name,
                        Description = p_cat.p.Description,
                        Price = p_cat.p.Price
                    }
                }).ToList();

            return data;
        }

        public bool CheckIDExist(string saleProductId)
        {
            var checkID = context.SaleProducts.AsNoTracking().FirstOrDefault(e => e.saleProductId == saleProductId);
            if (checkID == null)
            {
                return false;
            }
            return true;
        }
    }
}
