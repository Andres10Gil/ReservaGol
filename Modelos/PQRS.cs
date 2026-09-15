using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class PQRS
    {
        [Key]
        public Guid Id_Pqrs { get; set; }

        public Guid Id_Usuario { get; set; }

        public string Tipo { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha_envio { get; set; }

        public string Estado { get; set; }

        public string Respuesta { get; set; }

        // Navegación
        public Usuario Usuario { get; set; }
    }
}
