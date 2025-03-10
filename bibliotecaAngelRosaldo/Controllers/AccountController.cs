using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bibliotecaAngelRosaldo.Models.ViewModels;
using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Context;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaAngelRosaldo.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            _logger.LogDebug("Login method called."); // Mensaje de depuración

            if (ModelState.IsValid)
            {
                var user = await _context.Set<Usuario>()
                    .Include(u => u.Roles) // Incluir la relación de roles
                    .FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Password == model.Password);

                if (user != null)
                {
                    _logger.LogInformation("Usuario {UserName} ha iniciado sesión exitosamente.", model.UserName);

                    // Crear los claims del usuario
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Roles.Nombre)
                    };

                    // Crear el ClaimsIdentity
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Crear el ClaimsPrincipal
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Establecer la cookie de autenticación
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    // Verificar si el usuario es administrador
                    if (user.Roles.Nombre == "Admin")
                    {
                        return RedirectToAction("Index", "Usuario");
                    }

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    _logger.LogWarning("Intento de inicio de sesión fallido para el usuario {UserName}.", model.UserName);
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    Nombre = model.Nombre,
                    UserName = model.UserName,
                    Password = model.Password,
                    FkRol = 2 // Asignar el rol de usuario por defecto
                };

                _context.Set<Usuario>().Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Cerrar la sesión del usuario
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("Usuario ha cerrado sesión.");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}