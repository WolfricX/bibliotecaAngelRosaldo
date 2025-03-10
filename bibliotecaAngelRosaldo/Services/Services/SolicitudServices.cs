using bibliotecaAngelRosaldo.Context;
using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaAngelRosaldo.Services.Services
{
    public class SolicitudServices : ISolicitudServices
    {
        private readonly ApplicationDbContext _context;
        public SolicitudServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Solicitud> GetAllSolicitudes()
        {
            return _context.Solicitudes.Include(x => x.Usuario).Include(x => x.Libro).ToList();
        }
        public Solicitud GetSolicitudById(int id)
        {
            return _context.Solicitudes.Find(id)!;
        }
        public bool CreateSolicitud(Solicitud request)
        {
            _context.Solicitudes.Add(request);
            return _context.SaveChanges() > 0;
        }
        public bool UpdateSolicitud(Solicitud request)
        {
            _context.Solicitudes.Update(request);
            return _context.SaveChanges() > 0;
        }
        public bool DeleteSolicitud(int id)
        {
            var solicitud = _context.Solicitudes.Find(id);
            if (solicitud != null)
            {
                _context.Solicitudes.Remove(solicitud);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public bool CrearPrestamoDesdeSolicitud(int solicitudId)
        {
            var solicitud = _context.Solicitudes.Include(s => s.Usuario).Include(s => s.Libro).FirstOrDefault(s => s.PkSolicitud == solicitudId);
            if (solicitud == null)
            {
                return false;
            }

            var prestamo = new Prestamo
            {
                FkUsuario = solicitud.FkUsuario,
                Usuario = solicitud.Usuario,
                FkLibro = solicitud.FkLibro,
                Libro = solicitud.Libro,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = null
            };

            _context.Prestamos.Add(prestamo);
            return _context.SaveChanges() > 0;
        }

        public async Task CreateSolicitudAsync(Solicitud solicitud)
        {
            _context.Solicitudes.Add(solicitud);
            await _context.SaveChangesAsync();
        }

        public List<Solicitud> GetSolicitudesByUsuario(int usuarioId)
        {
            return _context.Solicitudes
                .Include(s => s.Libro)
                .Where(s => s.FkUsuario == usuarioId)
                .ToList();
        }
    }
}