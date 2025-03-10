using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaAngelRosaldo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaServices _categoriaServices;

        public CategoriaController(ICategoriaServices categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        public IActionResult Index()
        {
            var result = _categoriaServices.ObtenerCategorias();
            return View(result);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Categoria request)
        {
            _categoriaServices.CrearCategoria(request);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var result = _categoriaServices.GetCategoriaById(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Editar(Categoria request)
        {
            _categoriaServices.EditarCategoria(request);
            return RedirectToAction("Index");
        }

        [HttpPost] // Cambiar de HttpDelete a HttpPost
        public IActionResult Eliminar(int id)
        {
            _categoriaServices.EliminarCategoria(id);
            return RedirectToAction("Index");
        }
    }
}