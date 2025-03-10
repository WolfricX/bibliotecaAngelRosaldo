using bibliotecaAngelRosaldo.Context;
using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaAngelRosaldo.Services.Services
{
    public class LibroServices : ILibroServices
    {
        private readonly ApplicationDbContext _context;
        public LibroServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Libro> ObtenerLibros()
        {
            return _context.Libros.Include(x => x.Categoria).ToList();
        }

        public Libro GetLibroById(int id)
        {
            return _context.Libros.Find(id)!;
        }

        public bool CrearLibro(Libro request)
        {
            _context.Libros.Add(request);
            return _context.SaveChanges() > 0;
        }

        public bool EditarLibro(Libro request)
        {
            _context.Libros.Update(request);
            return _context.SaveChanges() > 0;
        }

        public bool EliminarLibro(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}