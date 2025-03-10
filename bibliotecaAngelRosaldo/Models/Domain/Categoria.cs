using System.ComponentModel.DataAnnotations;

namespace bibliotecaAngelRosaldo.Models.Domain
{
    public class Categoria
    {
        [Key]
        public int PkCategoria { get; set; }
        public string Nombre { get; set; }
    }
}