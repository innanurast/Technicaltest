﻿using Microsoft.AspNetCore.Mvc;

namespace Front_end.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}