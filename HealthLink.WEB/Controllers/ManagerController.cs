﻿using Microsoft.AspNetCore.Mvc;

namespace HealthLink.WEB.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
