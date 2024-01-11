using Microsoft.AspNetCore.Mvc;

namespace Front_end.Controllers
{
    public class TestCors : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
