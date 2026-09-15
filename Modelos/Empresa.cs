using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class Empresa
    {
        [Key]
        public Guid Id_Empresa { get; set; }

        public Guid Id_Usuario { get; set; }

        public string Nombre { get; set; }

        public int Nit { get; set; }

        public string Direccion { get; set; }

        public string Ciudad { get; set; }

        public int Telefono { get; set; }

        public string Correo { get; set; }

        public DateTime Fecha_creacion { get; set; }

        public bool Activo { get; set; }

        // Navegación
        public Usuario Usuario { get; set; }
        public ICollection<Equipamiento> Equipamientos { get; set; }
        public ICollection<EventoPromocion> EventosPromociones { get; set; }
        public ICollection<ReporteEstadistico> ReportesEstadisticos { get; set; }
    }
}
