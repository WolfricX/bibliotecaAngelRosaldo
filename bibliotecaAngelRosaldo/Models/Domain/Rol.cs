using System.ComponentModel.DataAnnotations;

namespace bibliotecaAngelRosaldo.Models.Domain
{
    public class Rol
    {
        [Key]
        public int PkRol { get; set; }
        public string Nombre { get; set; }

    }
}
