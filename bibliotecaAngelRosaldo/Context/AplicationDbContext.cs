using bibliotecaAngelRosaldo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaAngelRosaldo.Context
{
    public class AplicationDbContext
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions options) : base(options) { }
            //Modelos a mapear
            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Rol> Roles { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Usuario>().HasData(
                    new Usuario
                    {
                        PkUsuario = 1,
                        Nombre = "Dano",
                        UserName = "Usuario",
                        Password = "12323",
                        FkRol = 1
                    }
                    );
                modelBuilder.Entity<Rol>().HasData(
                    new Rol
                    {
                        PkRol = 1,
                        Nombre = "Admin"
                    });

            }
        }
    }
}