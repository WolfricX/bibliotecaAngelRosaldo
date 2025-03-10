using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bibliotecaAngelRosaldo.Models.Domain
{
    public class Solicitud
    {
        [Key]
        public int PkSolicitud { get; set; }

        [Required]
        public int FkUsuario { get; set; }

        [ForeignKey("FkUsuario")]
        public Usuario Usuario { get; set; }

        [Required]
        public int FkLibro { get; set; }

        [ForeignKey("FkLibro")]
        public Libro Libro { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Required]
        [StringLength(50)]
        public string EstadoSolicitud { get; set; }
    }

}