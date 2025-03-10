using bibliotecaAngelRosaldo.Context;
using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaAngelRosaldo.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDbContext _context;
        public UsuarioServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Usuario> ObtenerUsuario()
        {
            try
            {
                List<Usuario> result = _context.Usuarios.Include(x => x.Roles).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }
        }

        public Usuario? GetUsuariobyId(int id)
        {
            try
            {
                Usuario? result = _context.Usuarios.Find(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario: " + ex.Message);
            }
        }

        public bool CrearUsuario(Usuario request)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRol = 1
                };
                _context.Usuarios.Add(usuario);
                int result = _context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario: " + ex.Message);
            }
        }

        public bool EditarUsuario(Usuario request)
        {
            try
            {
                Usuario? usuario = _context.Usuarios.Find(request.PkUsuario);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                usuario.Nombre = request.Nombre;
                usuario.UserName = request.UserName;
                usuario.Password = request.Password;
                usuario.FkRol = request.FkRol;
                _context.Usuarios.Update(usuario);
                int result = _context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el usuario: " + ex.Message);
            }
        }

        public bool EliminarUsuario(int id)
        {
            try
            {
                var usuario = _context.Usuarios.Find(id);
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                    int result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        public List<Rol> ObtenerRoles()
        {
            try
            {
                return _context.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles: " + ex.Message);
            }
        }

    }
}

