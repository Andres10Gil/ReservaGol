using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class EventoPromocion
    {
        [Key]
        public Guid Id_Evento { get; set; }

        public Guid Id_Empresa { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha_inicio { get; set; }

        public DateTime Fecha_fin { get; set; }

        public decimal Descuento { get; set; }

        public bool Activo { get; set; }

        // Navegación
        public Empresa Empresa { get; set; }
    }
}
