using bibliotecaAngelRosaldo.Models.Domain;
using System.Collections.Generic;

namespace bibliotecaAngelRosaldo.Services.IServices
{
    public interface IPrestamoServices
    {
        List<Prestamo> ObtenerPrestamos();
        Prestamo GetPrestamoById(int id);
        bool CrearPrestamo(Prestamo request);
        bool EditarPrestamo(Prestamo request);
        bool EliminarPrestamo(int id);
    }
}