﻿
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagGroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}