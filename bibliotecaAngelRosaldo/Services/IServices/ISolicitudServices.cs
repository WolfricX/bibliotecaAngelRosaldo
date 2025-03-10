using bibliotecaAngelRosaldo.Models.Domain;

namespace bibliotecaAngelRosaldo.Services.IServices
{
    public interface ISolicitudServices
    {
        List<Solicitud> GetAllSolicitudes();
        Solicitud GetSolicitudById(int id);
        bool CreateSolicitud(Solicitud request);
        bool UpdateSolicitud(Solicitud request);
        bool DeleteSolicitud(int id);
        bool CrearPrestamoDesdeSolicitud(int solicitudId);
        Task CreateSolicitudAsync(Solicitud solicitud); // Agregar esta línea
        List<Solicitud> GetSolicitudesByUsuario(int usuarioId); // Agregar esta línea
    }
}