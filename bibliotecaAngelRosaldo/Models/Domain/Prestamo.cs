using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bibliotecaAngelRosaldo.Models.Domain
{
    public class Prestamo
    {
        [Key]
        public int PkPrestamo { get; set; }

        [ForeignKey("Usuario")]
        public int FkUsuario { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Libro")]
        public int FkLibro { get; set; }
        public Libro Libro { get; set; }

        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
    }
}