using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class Cancha
    {
        [Key]
        public Guid Id_Canchas { get; set; }

        public string Nombre { get; set; }

        public string Ubicacion { get; set; }

        public string Dimenciones { get; set; }

        public decimal Precio_Hora { get; set; }

        // Navegación
        public ICollection<Reserva> Reservas { get; set; }
    }
}
