using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReservaGol.context;
using ReservaGol.Repositorios;
using ReservaGol.Repositorios.Interfaces;
using ReservaGol.Servicios;
using ReservaGol.Servicios.Interfaces;
using System.Text;

namespace ReservaGol
{
    public static class DependenciasInyeccion
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            // Base de datos
            services.AddDbContext<BdReservaGolContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IRolesRepositorio, RolesRepositorio>();
            services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
            services.AddScoped<ICanchaRepositorio, CanchaRepositorio>();

            // Servicios
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();

            // JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                    };
                });

            return services;
        }
    }
}
