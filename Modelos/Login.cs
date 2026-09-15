using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class Login
    {
        [Required]
        public string NombreUsuario { get; set; }  = string.Empty;


        [Required]
        public string Contraseña { get; set; } = string.Empty;



    }
}
