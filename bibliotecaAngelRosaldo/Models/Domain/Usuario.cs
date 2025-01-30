using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bibliotecaAngelRosaldo.Models.Domain
{
    public class Usuario
    {
        [Key]
        public int PkUsuario { get; set; }
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [ForeignKey("Roles")]
        public int FkRol { get; set; }
        public Rol Roles { get; set; }

    }
}
