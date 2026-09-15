using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class ReporteEstadistico
    {
        [Key]
        public Guid Id_Reporte { get; set; }

        public Guid IdEmpresas { get; set; }

        public DateTime Fecha_generacion { get; set; }

        public string Tipo_reporte { get; set; }

        public DateTime Periodo_inicio { get; set; }

        public DateTime Periodo_fin { get; set; }

        public int Total_reservas { get; set; }

        public decimal Total_ingresos { get; set; }

        public string Cancha_mas_reservada { get; set; }

        public string Usuario_mas_activo { get; set; }

        public decimal Tasa_ocupacion { get; set; }

        public string Comentarios { get; set; }

        // Navegación
        public Empresa Empresa { get; set; }
    }
}
