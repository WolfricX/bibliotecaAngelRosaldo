using bibliotecaAngelRosaldo.Context;
using bibliotecaAngelRosaldo.Models.Domain;
using bibliotecaAngelRosaldo.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace bibliotecaAngelRosaldo.Services.Services
{
    public class CategoriaServices : ICategoriaServices
    {
        private readonly ApplicationDbContext _context;

        public CategoriaServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Categoria> ObtenerCategorias()
        {
            return _context.Categorias.ToList();
        }

        public Categoria GetCategoriaById(int id)
        {
            return _context.Categorias.Find(id);
        }

        public bool CrearCategoria(Categoria request)
        {
            _context.Categorias.Add(request);
            return _context.SaveChanges() > 0;
        }

        public bool EditarCategoria(Categoria request)
        {
            _context.Categorias.Update(request);
            return _context.SaveChanges() > 0;
        }

        public bool EliminarCategoria(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}