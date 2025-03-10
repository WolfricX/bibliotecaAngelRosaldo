using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaAngelRosaldo.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroServices _libroServices;
        private readonly ICategoriaServices _categoriaServices;

        public LibroController(ILibroServices libroServices, ICategoriaServices categoriaServices)
        {
            _libroServices = libroServices;
            _categoriaServices = categoriaServices;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var result = _libroServices.ObtenerLibros();
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Categorias = _categoriaServices.ObtenerCategorias();
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Crear(Libro request)
        {
            _libroServices.CrearLibro(request);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var result = _libroServices.GetLibroById(id);
            ViewBag.Categorias = _categoriaServices.ObtenerCategorias();
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Editar(Libro request)
        {
            _libroServices.EditarLibro(request);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            _libroServices.EliminarLibro(id);
            return RedirectToAction("Index");
        }
    }
}