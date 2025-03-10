using bibliotecaAngelRosaldo.Models.Domain;

public interface ICategoriaServices
{
    List<Categoria> ObtenerCategorias();
    Categoria GetCategoriaById(int id);
    bool CrearCategoria(Categoria request);
    bool EditarCategoria(Categoria request);
    bool EliminarCategoria(int id);
}