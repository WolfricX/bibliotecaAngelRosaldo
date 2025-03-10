using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaAngelRosaldo.Controllers
{
    [Authorize]
    public class SolicitudController : Controller
    {
        private readonly ISolicitudServices _solicitudServices;
        private readonly IUsuarioServices _usuarioServices;
        private readonly ILibroServices _libroServices;
        public SolicitudController(ISolicitudServices solicitudServices, IUsuarioServices usuarioServices, ILibroServices libroServices)
        {
            _solicitudServices = solicitudServices;
            _usuarioServices = usuarioServices;
            _libroServices = libroServices;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var result = _solicitudServices.GetAllSolicitudes();
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Crear()
        {
            ViewBag.Usuarios = _usuarioServices.ObtenerUsuario();
            ViewBag.Libros = _libroServices.ObtenerLibros();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Crear(Solicitud request)
        {
            _solicitudServices.CreateSolicitud(request);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Editar(int id)
        {
            var result = _solicitudServices.GetSolicitudById(id);
            ViewBag.Usuarios = _usuarioServices.ObtenerUsuario();
            ViewBag.Libros = _libroServices.ObtenerLibros();
            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Editar(Solicitud request)
        {
            _solicitudServices.UpdateSolicitud(request);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Eliminar(int id)
        {
            _solicitudServices.DeleteSolicitud(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CrearPrestamoDesdeSolicitud(int solicitudId)
        {
            var solicitud = _solicitudServices.GetSolicitudById(solicitudId);
            if (solicitud == null)
            {
                return NotFound();
            }

            // Crear el préstamo
            var prestamoCreado = _solicitudServices.CrearPrestamoDesdeSolicitud(solicitudId);
            if (!prestamoCreado)
            {
                return BadRequest("No se pudo crear el préstamo.");
            }

            // Actualizar el estado de la solicitud
            solicitud.EstadoSolicitud = "Prestamo Realizado";
            var solicitudActualizada = _solicitudServices.UpdateSolicitud(solicitud);
            if (!solicitudActualizada)
            {
                return BadRequest("No se pudo actualizar el estado de la solicitud.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Solicitar(int libroId)
        {
            var userName = User.Identity?.Name;

            if (userName == null)
            {
                return Unauthorized();
            }

            var usuario = _usuarioServices.ObtenerUsuario().FirstOrDefault(u => u.UserName == userName);

            if (usuario == null)
            {
                return Unauthorized();
            }

            var solicitud = new Solicitud
            {
                FkUsuario = usuario.PkUsuario,
                FkLibro = libroId,
                FechaSolicitud = DateTime.Now,
                EstadoSolicitud = "Prestamo Pendiente"
            };

            await _solicitudServices.CreateSolicitudAsync(solicitud);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult MisSolicitudes()
        {
            var userName = User.Identity?.Name;

            if (userName == null)
            {
                return Unauthorized();
            }

            var usuario = _usuarioServices.ObtenerUsuario().FirstOrDefault(u => u.UserName == userName);

            if (usuario == null)
            {
                return Unauthorized();
            }

            var solicitudes = _solicitudServices.GetSolicitudesByUsuario(usuario.PkUsuario);
            return View("~/Views/Home/MisSolicitudes.cshtml", solicitudes); // Especificar la ruta completa de la vista
        }
    }
}