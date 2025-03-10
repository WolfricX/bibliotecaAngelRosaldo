using bibliotecaAngelRosaldo.Models.Domain;

namespace bibliotecaAngelRosaldo.Services.IServices
{
    public interface ILibroServices
    {
        List<Libro> ObtenerLibros();
        Libro GetLibroById(int id);
        bool CrearLibro(Libro request);
        bool EditarLibro(Libro request);
        bool EliminarLibro(int id);
    }
}