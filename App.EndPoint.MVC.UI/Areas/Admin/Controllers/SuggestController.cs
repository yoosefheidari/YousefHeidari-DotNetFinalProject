﻿using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SuggestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
