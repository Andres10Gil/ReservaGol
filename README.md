# ReservaGol

API en ASP.NET Core 8 para la gestión de reservas de canchas deportivas (usuarios, canchas, reservas, roles, autenticación JWT).

## Arquitectura

- **Controladores** → **Repositorios** (interfaz + implementación) → **DbContext** (Entity Framework Core / SQL Server)
- **Servicios**: hash de contraseñas (BCrypt) y generación de JWT, desacoplados de los controladores
- **Middleware** de manejo centralizado de excepciones

## Requisitos

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Puesta en marcha (local, con Docker)

1. Copia el archivo de variables de entorno de ejemplo y ajusta los valores:

   ```bash
   cp .env.example .env
   ```

2. Levanta los contenedores:

   ```bash
   docker compose up -d --build
   ```

   Esto levanta tres servicios:
   - **app**: la API ASP.NET Core → http://localhost:8080/swagger
   - **sqlserver**: SQL Server 2022, con el esquema importado automáticamente desde `sql/init/01_init.sql`
   - **adminer**: interfaz web para explorar la base de datos → http://localhost:8888 (servidor `sqlserver`, usuario `sa`, contraseña la definida en `.env`)

3. Para detener todo:

   ```bash
   docker compose down
   ```

   Agrega `-v` si además quieres borrar los datos de la base de datos.

## Variables de entorno

Ver [`.env.example`](.env.example). El archivo `.env` real (con tus credenciales) nunca se sube al repositorio.

## Estructura del proyecto

```
Controladores/      Endpoints HTTP (API)
Repositorios/        Acceso a datos (EF Core), con sus interfaces
Servicios/            Lógica de dominio reutilizable (JWT, hash de contraseñas)
Middleware/           Manejo centralizado de errores
Modelos/               Entidades de dominio / EF
context/                DbContext de Entity Framework
sql/                     Script de inicialización de la base de datos
```
