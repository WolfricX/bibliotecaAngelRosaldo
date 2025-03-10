using bibliotecaAngelRosaldo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaAngelRosaldo.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Modelos a mapear
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }

    }
}