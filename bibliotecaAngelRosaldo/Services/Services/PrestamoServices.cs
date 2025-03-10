using bibliotecaAngelRosaldo.Context;
using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace bibliotecaAngelRosaldo.Services.Services
{
    public class PrestamoServices : IPrestamoServices
    {
        private readonly ApplicationDbContext _context;
        public PrestamoServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Prestamo> ObtenerPrestamos()
        {
            return _context.Prestamos.Include(x => x.Usuario).Include(x => x.Libro).ToList();
        }

        public Prestamo GetPrestamoById(int id)
        {
            return _context.Prestamos.Find(id)!;
        }

        public bool CrearPrestamo(Prestamo request)
        {
            _context.Prestamos.Add(request);
            return _context.SaveChanges() > 0;
        }

        public bool EditarPrestamo(Prestamo request)
        {
            _context.Prestamos.Update(request);
            return _context.SaveChanges() > 0;
        }

        public bool EliminarPrestamo(int id)
        {
            var prestamo = _context.Prestamos.Find(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}