using Microsoft.AspNetCore.Mvc;
using SalesApp.Repository;
using SalesApp.ViewModel;
using System.Net;

namespace SalesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleproductController : ControllerBase
    {
        private readonly SaleproductRepository repository;

        public SaleproductController(SaleproductRepository repository)
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

        [HttpGet("{saleProductId}")]
        public virtual ActionResult Get(string saleProductId)
        {
            var data = repository.Get(saleProductId);
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
        public virtual ActionResult Insert(SaleproductVm saleproduct)
        {
            var data = repository.Insert(saleproduct);
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil ditambahkan.", data });
        }

        [HttpPut("{saleProductId}")]
        public virtual ActionResult Update(string saleProductId, SaleproductVm saleproduct)
        {
            if (repository.CheckIDExist(saleProductId) == false)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Id tidak ditemukan." });
            }
            var data = repository.Update(saleProductId, saleproduct);
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil diubah.", data });
        }

        [HttpDelete("{saleProductId}")]
        public virtual ActionResult Delete(string saleProductId)
        {
            if (repository.CheckIDExist(saleProductId) == false)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "ID tidak ditemukan." });
            }

            repository.Delete(saleProductId);
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil dihapus." });
        }
    }
}
