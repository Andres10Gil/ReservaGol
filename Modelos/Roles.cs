using System.ComponentModel.DataAnnotations;

namespace ReservaGol.Modelos
{
    public class Roles
    {
        [Key]
        public Guid Id_Roles { get; set; }

        public string Nombre_rol { get; set; }

        public string Descripcion { get; set; }

        public int Nivel_acceso { get; set; }

        public bool Activo { get; set; }

        public DateTime Creando_em { get; set; }

        // Navegación
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
