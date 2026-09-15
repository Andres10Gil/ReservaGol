using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class PagoDetalle
    {
        [Key]
        public Guid Id_Pago { get; set; }

        public Guid Id_Factura { get; set; }

        public DateTime Fecha_pago { get; set; }

        public decimal Monto { get; set; }

        public string Metodo { get; set; }

        public string Estado { get; set; }

        public string Referencia { get; set; }

        // Navegación
        public Facturacion Facturacion { get; set; }
    }
}
