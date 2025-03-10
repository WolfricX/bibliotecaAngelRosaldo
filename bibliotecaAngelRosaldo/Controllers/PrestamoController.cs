using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaAngelRosaldo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PrestamoController : Controller
    {
        private readonly IPrestamoServices _prestamoServices;
        private readonly ISolicitudServices _solicitudServices;

        public PrestamoController(IPrestamoServices prestamoServices, ISolicitudServices solicitudServices)
        {
            _prestamoServices = prestamoServices;
            _solicitudServices = solicitudServices;
        }

        public IActionResult Index()
        {
            var result = _prestamoServices.ObtenerPrestamos();
            return View(result);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var result = _prestamoServices.GetPrestamoById(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Editar(Prestamo request)
        {
            _prestamoServices.EditarPrestamo(request);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            _prestamoServices.EliminarPrestamo(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarcarComoDevuelto(int id)
        {
            var prestamo = _prestamoServices.GetPrestamoById(id);
            if (prestamo != null)
            {
                prestamo.FechaDevolucion = DateTime.Now;
                var resultado = _prestamoServices.EditarPrestamo(prestamo);
                if (resultado)
                {
                    return RedirectToAction("Index");
                }
            }
            return BadRequest("No se pudo marcar el préstamo como devuelto.");
        }
    }
}