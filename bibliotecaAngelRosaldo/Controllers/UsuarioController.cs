using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bibliotecaAngelRosaldo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }
        public IActionResult Index()
        {
            var result = _usuarioServices.ObtenerUsuario();
            return View(result);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Usuario request)
        {
            _usuarioServices.CrearUsuario(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var result = _usuarioServices.GetUsuariobyId(id);
            var roles = _usuarioServices.ObtenerRoles(); // Obtener la lista de roles
            ViewBag.Roles = roles.Select(r => new SelectListItem
            {
                Value = r.PkRol.ToString(),
                Text = r.Nombre
            }).ToList();
            return View(result);
        }

        [HttpPost]
        public IActionResult Editar(Usuario request)
        {
            _usuarioServices.EditarUsuario(request);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            bool result = _usuarioServices.EliminarUsuario(id);
            return Json(new { success = result });
        }

    }
}
