using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebCafe.Models;

namespace WebCafe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SobreNos()
        {
            return View();
        }

        public IActionResult NossaMissao()
        {
            return View();
        }

        public IActionResult DuvidasFrequentes()
        {
            return View();
        }

        public IActionResult FaleConosco()
        {
            return View();
        }
        public IActionResult Portfolio()
        {
            return View();
        }
        public IActionResult Carrinho()
        {
            return View();
        }
    }
}
