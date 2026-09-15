using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class Equipamiento
    {
        [Key]
        public Guid Id_Equipo { get; set; }

        public Guid Id_Empresa { get; set; }

        public string Nombre_equipo { get; set; }

        public string Descripcion { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; }

        // Navegación
        public Empresa Empresa { get; set; }
    }
}
