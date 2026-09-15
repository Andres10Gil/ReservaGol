using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class Facturacion
    {
        [Key]
        public Guid Id_Factura { get; set; }

        public Guid Id_Reserva { get; set; }

        public Guid Id_Usuario { get; set; }

        public DateTime Fecha_factura { get; set; }

        public string Metodo_pago { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Impuestos { get; set; }

        public decimal Total { get; set; }

        public string Estado_pago { get; set; }

        public string Referencia_transaccion { get; set; }

        // Navegación
        public Reserva Reserva { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<PagoDetalle> PagosDetalle { get; set; }
    }
}
