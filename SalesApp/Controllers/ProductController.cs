using Microsoft.AspNetCore.Mvc;
using SalesApp.Model;
using SalesApp.Repository;
using SalesApp.ViewModel;
using System.Net;

namespace SalesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ProductRepository repository;

        public ProductController(ProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = repository.Get(); // mengambil fungsi dari repositori
            if (data.Count() != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil ditemukan.", data });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan." });
            }
        }

        [HttpGet("{ProductId}")]
        public virtual ActionResult Get(string ProductId)
        {
            var data = repository.Get(ProductId);
            if (data != null)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil ditemukan.", data });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan." });

            }
        }

        [HttpPost]
        public virtual ActionResult Insert(ProductVm product)
        {
            var data = repository.Insert(product);
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil ditambahkan.", data});
        }

        [HttpPut("{ProductId}")]
        public virtual ActionResult Update(string ProductId, ProductVm product)
        {
            if (repository.CheckIDExist(ProductId) == false)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "ID tidak ditemukan." });
            }
            var data = repository.Update(ProductId, product);
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil diubah.", data });
        }

        [HttpDelete("{ProductId}")]
        public virtual ActionResult Delete(string ProductId)
        {
            if (repository.CheckIDExist(ProductId) == false)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "ID tidak ditemukan." });
            }
            // Cek apakah ada referensi di tabel SaleProduct
            if (repository.HasSaleProductReferences(ProductId))
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Tidak dapat menghapus produk karena masih memiliki referensi di tabel SaleProduct." });
            }

            repository.Delete(ProductId);
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil dihapus." });
        }

        //[HttpPost("paging")]
        //public ActionResult GetData(JqueryDatatableParam  param)
        //{
        //    var draw = param.Draw;
        //    var start = param.start;
        //    var length = param.length;
        //    var searchValue = param.search?.value; // Ambil nilai pencarian

        //    // Pass the sorting and searching parameters to the repository
        //    var products = repository.GetProducts(start, length, searchValue);

        //    // Calculate the total records and total display records based on actual data
        //    var totalRecords = repository.GetTotalRecord();
        //    var totalDisplayRecords = repository.GetTotalDisplayRecords(searchValue);


        //    return Ok(new
        //    {
        //        draws = draw,
        //        recordsTotal = totalRecords, // Total jumlah karyawan
        //        recordsFiltered = totalDisplayRecords, // Total jumlah karyawan setelah filter (tidak ada filter dalam contoh ini)
        //        data = products
        //    });
        //}
    }
}
