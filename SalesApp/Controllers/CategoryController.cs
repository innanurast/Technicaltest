using SalesApp.Model;
using SalesApp.ViewModel;
using SalesApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq.Dynamic.Core;

namespace SalesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController :  ControllerBase
    {
        private readonly CategoryRepository repository;
        public CategoryController(CategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = repository.Get();
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil ditemukan.", data });
        }

        [HttpGet("{id}")]
        public virtual ActionResult Get(int Id)
        {
            var data = repository.Get(Id);
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
        public virtual ActionResult Insert(CategoryVm category)
        {
            repository.Insert(category);
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil ditambahkan." });
        }

        [HttpDelete("{id}")]
        public virtual ActionResult Delete(int Id)
        {
            var delete = repository.Delete(Id);
            if (delete >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Dihapus", Data = delete });
            }
            else if (delete == 0)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data dengan Id " + Id + "Tidak Ditemukan", Data = delete });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Terjadi Kesalahan", Data = delete });
            }
        }

        [HttpPut]
        public virtual ActionResult Update(CategoryVm category)
        {
            repository.Update(category);
            return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil diubah." });
        }
    }
}
