using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index()
        {
            //return RedirectToAction("Register","Account");
            return View();
        }

      

      
    }
}
