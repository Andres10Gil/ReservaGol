using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaGol.Modelos
{
    public class Usuario
    {
        [Key]
        public Guid Id_Usuario { get; set; }

        public Guid Id_Roles { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public int Telefono { get; set; }

        public string Contraseña { get; set; }

        public DateTime Fecha_registro { get; set; }

        // Navegación
        public Roles Roles { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<Facturacion> Facturaciones { get; set; }
        public ICollection<PQRS> PQRS { get; set; }
        public ICollection<Empresa> Empresas { get; set; }
    }
}
