using Microsoft.AspNetCore.Mvc;
using SuperLogica.Interfaces;
using SuperLogica.Models;
using System.Diagnostics;

namespace SuperLogica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly IContatoRepositorio _contatoRepositorio;
        public HomeController(IContatoRepositorio contatoRepositorio) => _contatoRepositorio = contatoRepositorio;


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}