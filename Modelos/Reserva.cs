using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class Reserva
    {
        [Key]
        public Guid Id_Reserva { get; set; }

        public Guid Id_Usuario { get; set; }

        public Guid Id_Cancha { get; set; }

        public DateTime Fecha_reserva { get; set; }

        public TimeSpan Hora_inicio { get; set; }

        public TimeSpan Hora_fin { get; set; }

        public string Estado { get; set; }

        // Navegación
        public Usuario Usuario { get; set; }
        public Cancha Cancha { get; set; }
        public ICollection<Facturacion> Facturaciones { get; set; }
    }
}
