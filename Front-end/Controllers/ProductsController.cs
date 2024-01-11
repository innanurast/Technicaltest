using Microsoft.AspNetCore.Mvc;

namespace Front_end.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
