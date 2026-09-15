USE [master]
GO
IF DB_ID(N'ReservaGolDB') IS NULL
BEGIN
    CREATE DATABASE [ReservaGolDB];
END
GO
ALTER DATABASE [ReservaGolDB] SET COMPATIBILITY_LEVEL = 150
GO
USE [ReservaGolDB]
GO
/****** Object:  Table [dbo].[Canchas]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Canchas](
	[Id_Canchas] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Ubicacion] [nvarchar](250) NOT NULL,
	[Dimenciones] [nvarchar](50) NOT NULL,
	[Precio_Hora] [decimal](18, 0) NOT NULL,
	[ImagenUrl] [nvarchar](500) NULL,
 CONSTRAINT [PK_Canchas] PRIMARY KEY CLUSTERED
(
	[Id_Canchas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresas]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresas](
	[Id_Empresa] [uniqueidentifier] NOT NULL,
	[Id_Usuario] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Nit] [int] NOT NULL,
	[Direccion] [nvarchar](100) NOT NULL,
	[Ciudad] [nvarchar](70) NOT NULL,
	[Telefono] [int] NOT NULL,
	[Correo] [nvarchar](250) NOT NULL,
	[Fecha_creacion] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Empresas] PRIMARY KEY CLUSTERED 
(
	[Id_Empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipamientos]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipamientos](
	[Id_Equipo] [uniqueidentifier] NOT NULL,
	[Id_Empresa] [uniqueidentifier] NOT NULL,
	[Nombre_equipo] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](250) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Equipamientos] PRIMARY KEY CLUSTERED 
(
	[Id_Equipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Eventos_Promociones]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eventos_Promociones](
	[Id_Evento] [uniqueidentifier] NOT NULL,
	[Id_Empresa] [uniqueidentifier] NOT NULL,
	[Titulo] [nvarchar](250) NOT NULL,
	[Descripcion] [nvarchar](255) NOT NULL,
	[Fecha_inicio] [datetime] NOT NULL,
	[Fecha_fin] [datetime] NOT NULL,
	[Descuento] [decimal](18, 0) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Eventos_Promociones] PRIMARY KEY CLUSTERED 
(
	[Id_Evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturacion]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturacion](
	[Id_Factura] [uniqueidentifier] NOT NULL,
	[Id_Reserva] [uniqueidentifier] NOT NULL,
	[Id_Usuario] [uniqueidentifier] NOT NULL,
	[Fecha_factura] [datetime] NOT NULL,
	[Metodo_pago] [nvarchar](50) NOT NULL,
	[Subtotal] [decimal](18, 0) NOT NULL,
	[Impuestos] [decimal](18, 0) NOT NULL,
	[Total] [decimal](18, 0) NOT NULL,
	[Estado_pago] [nvarchar](50) NOT NULL,
	[Referencia_transaccion] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Facturacion] PRIMARY KEY CLUSTERED 
(
	[Id_Factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagos_detalle]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos_detalle](
	[Id_Pago] [uniqueidentifier] NOT NULL,
	[Id_Factura] [uniqueidentifier] NOT NULL,
	[Fecha_pago] [datetime] NOT NULL,
	[Monto] [decimal](18, 0) NOT NULL,
	[Metodo] [nvarchar](50) NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
	[Referencia] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Pagos_detalle] PRIMARY KEY CLUSTERED 
(
	[Id_Pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PQRS]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PQRS](
	[Id_Pqrs] [uniqueidentifier] NOT NULL,
	[Id_Usuario] [uniqueidentifier] NOT NULL,
	[Tipo] [nvarchar](150) NOT NULL,
	[Descripcion] [nvarchar](250) NOT NULL,
	[Fecha_envio] [datetime] NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
	[Respuesta] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_PQRS] PRIMARY KEY CLUSTERED 
(
	[Id_Pqrs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reportes_estadisticos]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reportes_estadisticos](
	[Id_Reporte] [uniqueidentifier] NOT NULL,
	[IdEmpresas] [uniqueidentifier] NOT NULL,
	[Fecha_generacion] [datetime] NOT NULL,
	[Tipo_reporte] [nvarchar](200) NOT NULL,
	[Periodo_inicio] [datetime] NOT NULL,
	[Periodo_fin] [datetime] NOT NULL,
	[Total_reservas] [int] NOT NULL,
	[Total_ingresos] [decimal](18, 0) NOT NULL,
	[Cancha_mas_reservada] [nvarchar](250) NOT NULL,
	[Usuario_mas_activo] [nvarchar](200) NOT NULL,
	[Tasa_ocupacion] [decimal](18, 0) NOT NULL,
	[Comentarios] [text] NOT NULL,
 CONSTRAINT [PK_Reportes_estadisticos] PRIMARY KEY CLUSTERED 
(
	[Id_Reporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](
	[Id_Reserva] [uniqueidentifier] NOT NULL,
	[Id_Usuario] [uniqueidentifier] NOT NULL,
	[Id_Cancha] [uniqueidentifier] NOT NULL,
	[Fecha_reserva] [datetime] NOT NULL,
	[Hora_inicio] [time](7) NOT NULL,
	[Hora_fin] [time](7) NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 
(
	[Id_Reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id_Roles] [uniqueidentifier] NOT NULL,
	[Nombre_rol] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](250) NOT NULL,
	[Nivel_acceso] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Creando_em] [datetime] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id_Roles] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 18/11/2025 16:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id_Usuario] [uniqueidentifier] NOT NULL,
	[Id_Roles] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Correo] [varchar](50) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Contraseña] [varchar](250) NOT NULL,
	[Fecha_registro] [datetime] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Empresas]  WITH CHECK ADD  CONSTRAINT [FK_Empresas_Usuarios] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Empresas] CHECK CONSTRAINT [FK_Empresas_Usuarios]
GO
ALTER TABLE [dbo].[Equipamientos]  WITH CHECK ADD  CONSTRAINT [FK_Equipamientos_Empresas] FOREIGN KEY([Id_Empresa])
REFERENCES [dbo].[Empresas] ([Id_Empresa])
GO
ALTER TABLE [dbo].[Equipamientos] CHECK CONSTRAINT [FK_Equipamientos_Empresas]
GO
ALTER TABLE [dbo].[Eventos_Promociones]  WITH CHECK ADD  CONSTRAINT [FK_Eventos_Promociones_Empresas] FOREIGN KEY([Id_Empresa])
REFERENCES [dbo].[Empresas] ([Id_Empresa])
GO
ALTER TABLE [dbo].[Eventos_Promociones] CHECK CONSTRAINT [FK_Eventos_Promociones_Empresas]
GO
ALTER TABLE [dbo].[Pagos_detalle]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_detalle_Facturacion] FOREIGN KEY([Id_Factura])
REFERENCES [dbo].[Facturacion] ([Id_Factura])
GO
ALTER TABLE [dbo].[Pagos_detalle] CHECK CONSTRAINT [FK_Pagos_detalle_Facturacion]
GO
ALTER TABLE [dbo].[PQRS]  WITH CHECK ADD  CONSTRAINT [FK_PQRS_Usuarios] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[PQRS] CHECK CONSTRAINT [FK_PQRS_Usuarios]
GO
ALTER TABLE [dbo].[Reportes_estadisticos]  WITH CHECK ADD  CONSTRAINT [FK_Reportes_estadisticos_Empresas1] FOREIGN KEY([IdEmpresas])
REFERENCES [dbo].[Empresas] ([Id_Empresa])
GO
ALTER TABLE [dbo].[Reportes_estadisticos] CHECK CONSTRAINT [FK_Reportes_estadisticos_Empresas1]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Canchas] FOREIGN KEY([Id_Cancha])
REFERENCES [dbo].[Canchas] ([Id_Canchas])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Canchas]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Usuarios] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Usuarios]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([Id_Roles])
REFERENCES [dbo].[Roles] ([Id_Roles])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
-- Las FK de Facturacion hacia Usuarios/Reserva estaban invertidas en el script original
-- (Usuarios -> Facturacion y Reserva -> Facturacion), lo que impedía crear un usuario o una
-- reserva sin que ya existiera una factura con el mismo Id. Facturacion es quien referencia
-- a Usuarios y a Reserva, tal como lo modela BdReservaGolContext.
ALTER TABLE [dbo].[Facturacion]  WITH CHECK ADD  CONSTRAINT [FK_Facturacion_Usuarios] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Facturacion] CHECK CONSTRAINT [FK_Facturacion_Usuarios]
GO
ALTER TABLE [dbo].[Facturacion]  WITH CHECK ADD  CONSTRAINT [FK_Facturacion_Reserva] FOREIGN KEY([Id_Reserva])
REFERENCES [dbo].[Reserva] ([Id_Reserva])
GO
ALTER TABLE [dbo].[Facturacion] CHECK CONSTRAINT [FK_Facturacion_Reserva]
GO
USE [master]
GO
ALTER DATABASE [ReservaGolDB] SET  READ_WRITE
GO
