using bibliotecaAngelRosaldo.Models.Domain;

namespace bibliotecaAngelRosaldo.Services.IServices
{
    public interface IUsuarioServices
    {
        List<Usuario> ObtenerUsuario();
        bool CrearUsuario(Usuario request);
        Usuario GetUsuariobyId(int id);
        bool EditarUsuario(Usuario request);
        bool EliminarUsuario(int id);
        List<Rol> ObtenerRoles(); // Método para obtener roles

    }
}
