using bibliotecaAngelRosaldo.Models;
using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace bibliotecaAngelRosaldo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILibroServices _libroServices;

        public HomeController(ILibroServices libroServices)
        {
            _libroServices = libroServices;
        }

        public IActionResult Index()
        {
            IEnumerable<Libro> libros = _libroServices.ObtenerLibros();
            return View(libros);
        }

        public IActionResult MisSolicitudes()
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