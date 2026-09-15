using Microsoft.EntityFrameworkCore;
using ReservaGol.Modelos;

namespace ReservaGol.context
{
    public class BdReservaGolContext : DbContext
    {
        public BdReservaGolContext(DbContextOptions<BdReservaGolContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Cancha> Canchas { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Equipamiento> Equipamientos { get; set; }
        public DbSet<EventoPromocion> Eventos_Promociones { get; set; }
        public DbSet<Facturacion> Facturacion { get; set; }
        public DbSet<PagoDetalle> Pagos_detalle { get; set; }
        public DbSet<PQRS> PQRS { get; set; }
        public DbSet<ReporteEstadistico> Reportes_estadisticos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usuarios
            modelBuilder.Entity<Usuario>(e =>
            {
                e.ToTable("Usuarios");
                e.HasKey(x => x.Id_Usuario);
                e.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
                e.Property(x => x.Correo).IsRequired().HasMaxLength(50);
                e.Property(x => x.Contraseña).IsRequired().HasMaxLength(250);
                e.Property(x => x.Fecha_registro).HasColumnName("Fecha_registro");
                e.HasOne(x => x.Roles).WithMany(r => r.Usuarios).HasForeignKey(x => x.Id_Roles);
            });

            // Roles
            modelBuilder.Entity<Roles>(e =>
            {
                e.ToTable("Roles");
                e.HasKey(x => x.Id_Roles);
                e.Property(x => x.Nombre_rol).IsRequired().HasMaxLength(100).HasColumnName("Nombre_rol");
                e.Property(x => x.Descripcion).HasMaxLength(500);
                e.Property(x => x.Creando_em).HasColumnName("Creando_em");
            });

            // Canchas
            modelBuilder.Entity<Cancha>(e =>
            {
                e.ToTable("Canchas");
                e.HasKey(x => x.Id_Canchas);
                e.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
                e.Property(x => x.Ubicacion).HasMaxLength(500);
                e.Property(x => x.Dimenciones).HasMaxLength(100);
                e.Property(x => x.Precio_Hora).HasColumnType("decimal(18,2)");
            });

            // Reserva
            modelBuilder.Entity<Reserva>(e =>
            {
                e.ToTable("Reserva");
                e.HasKey(x => x.Id_Reserva);
                e.Property(x => x.Fecha_reserva).HasColumnName("Fecha_reserva");
                e.Property(x => x.Hora_inicio).HasColumnName("Hora_inicio");
                e.Property(x => x.Hora_fin).HasColumnName("Hora_fin");
                e.Property(x => x.Estado).HasMaxLength(100);
                e.HasOne(x => x.Usuario).WithMany(u => u.Reservas).HasForeignKey(x => x.Id_Usuario);
                e.HasOne(x => x.Cancha).WithMany(c => c.Reservas).HasForeignKey(x => x.Id_Cancha);
            });

            // Empresas
            modelBuilder.Entity<Empresa>(e =>
            {
                e.ToTable("Empresas");
                e.HasKey(x => x.Id_Empresa);
                e.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
                e.Property(x => x.Direccion).HasMaxLength(200);
                e.Property(x => x.Ciudad).HasMaxLength(140);
                e.Property(x => x.Correo).HasMaxLength(500);
                e.HasOne(x => x.Usuario).WithMany(u => u.Empresas).HasForeignKey(x => x.Id_Usuario);
            });

            // Equipamientos
            modelBuilder.Entity<Equipamiento>(e =>
            {
                e.ToTable("Equipamientos");
                e.HasKey(x => x.Id_Equipo);
                e.Property(x => x.Nombre_equipo).IsRequired().HasMaxLength(200);
                e.Property(x => x.Descripcion).HasMaxLength(500);
                e.Property(x => x.Estado).HasMaxLength(100);
                e.HasOne(x => x.Empresa).WithMany(em => em.Equipamientos).HasForeignKey(x => x.Id_Empresa);
            });

            // Eventos_Promociones
            modelBuilder.Entity<EventoPromocion>(e =>
            {
                e.ToTable("Eventos_Promociones");
                e.HasKey(x => x.Id_Evento);
                e.Property(x => x.Titulo).HasMaxLength(500);
                e.Property(x => x.Descripcion).HasMaxLength(510);
                e.Property(x => x.Descuento).HasColumnType("decimal(18,2)");
                e.HasOne(x => x.Empresa).WithMany(em => em.EventosPromociones).HasForeignKey(x => x.Id_Empresa);
            });

            // Facturacion
            modelBuilder.Entity<Facturacion>(e =>
            {
                e.ToTable("Facturacion");
                e.HasKey(x => x.Id_Factura);
                e.Property(x => x.Metodo_pago).HasMaxLength(100);
                e.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");
                e.Property(x => x.Impuestos).HasColumnType("decimal(18,2)");
                e.Property(x => x.Total).HasColumnType("decimal(18,2)");
                e.Property(x => x.Estado_pago).HasMaxLength(100);
                e.Property(x => x.Referencia_transaccion).HasMaxLength(200);
                e.HasOne(x => x.Reserva).WithMany(r => r.Facturaciones).HasForeignKey(x => x.Id_Reserva);
                e.HasOne(x => x.Usuario).WithMany(u => u.Facturaciones).HasForeignKey(x => x.Id_Usuario);
            });

            // Pagos_detalle
            modelBuilder.Entity<PagoDetalle>(e =>
            {
                e.ToTable("Pagos_detalle");
                e.HasKey(x => x.Id_Pago);
                e.Property(x => x.Monto).HasColumnType("decimal(18,2)");
                e.Property(x => x.Metodo).HasMaxLength(100);
                e.Property(x => x.Estado).HasMaxLength(100);
                e.Property(x => x.Referencia).HasMaxLength(200);
                e.HasOne(x => x.Facturacion).WithMany(f => f.PagosDetalle).HasForeignKey(x => x.Id_Factura);
            });

            // PQRS
            modelBuilder.Entity<PQRS>(e =>
            {
                e.ToTable("PQRS");
                e.HasKey(x => x.Id_Pqrs);
                e.Property(x => x.Tipo).HasMaxLength(300);
                e.Property(x => x.Descripcion).HasMaxLength(500);
                e.Property(x => x.Estado).HasMaxLength(100);
                e.Property(x => x.Respuesta).HasMaxLength(500);
                e.HasOne(x => x.Usuario).WithMany(u => u.PQRS).HasForeignKey(x => x.Id_Usuario);
            });

            // Reportes_estadisticos
            modelBuilder.Entity<ReporteEstadistico>(e =>
            {
                e.ToTable("Reportes_estadisticos");
                e.HasKey(x => x.Id_Reporte);
                e.Property(x => x.Tipo_reporte).HasMaxLength(400);
                e.Property(x => x.Total_ingresos).HasColumnType("decimal(18,2)");
                e.Property(x => x.Tasa_ocupacion).HasColumnType("decimal(18,2)");
                e.Property(x => x.Cancha_mas_reservada).HasMaxLength(500);
                e.Property(x => x.Usuario_mas_activo).HasMaxLength(400);
                e.Property(x => x.Comentarios).HasColumnType("text");
                e.HasOne(x => x.Empresa).WithMany(em => em.ReportesEstadisticos).HasForeignKey(x => x.IdEmpresas);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
