using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bibliotecaAngelRosaldo.Models.Domain
{
    public class Libro
    {
        [Key]
        public int PkLibro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public bool Disponible { get; set; }

        [ForeignKey("Categoria")]
        public int FkCategoria { get; set; }
        public Categoria Categoria { get; set; }
    }
}