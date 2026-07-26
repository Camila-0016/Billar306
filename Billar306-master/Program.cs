using Billar306.API.Configuration;
using Billar306.API.Data;
using Billar306.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Billar306.Aplicacion.Services;
using Billar306.Dominio.Interfaces;
using Billar306.Persistencia.Repositories;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

namespace Billar306.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. REGISTROS CRÍTICOS FALTANTES
            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autenticación JWT usando el esquema Bearer. \r\n\r\n Escribe 'Bearer' [espacio] y luego tu token.\r\n\r\nEjemplo: \"Bearer eyJhbGciOiJIUzI1Ni...\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            // CORS 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:3000",
                            "http://localhost:5500",
                            "http://127.0.0.1:5500",
                            "http://localhost:5174"

                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Base de datos SQLite
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // JWT
            var jwtKey = builder.Configuration["Jwt:Key"] ?? "billar306_clave_secreta_super_larga_2026";
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Billar306",
                        ValidAudience = "Billar306",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            // Repositories
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IDiaLaboralRepository, DiaLaboralRepository>();
            builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
            builder.Services.AddScoped<IRegistroTurnoEmpleadoRepository, RegistroTurnoEmpleadoRepository>();
            builder.Services.AddScoped<IMesaRepository, MesaRepository>();
            builder.Services.AddScoped<ISesionMesaRepository, SesionMesaRepository>();
            builder.Services.AddScoped<IConfiguracionSistemaRepository, ConfiguracionSistemaRepository>();
            builder.Services.AddScoped<ICatalogoRepository, CatalogoRepository>();
            builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
            builder.Services.AddScoped<IVentaConfiteriaRepository, VentaConfiteriaRepository>();
            builder.Services.AddScoped<IItemConfiteriaRepository, ItemConfiteriaRepository>();
            builder.Services.AddScoped<ICuentaBaseRepository, CuentaBaseRepository>();

            // Services
            builder.Services.AddScoped<ClienteService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<DiaLaboralService>();
            builder.Services.AddScoped<TurnoService>();
            builder.Services.AddScoped<MesaService>();
            builder.Services.AddScoped<SesionMesaService>();
            builder.Services.AddScoped<ConfiguracionSistemaService>();
            builder.Services.AddScoped<CatalogoService>();
            builder.Services.AddScoped<ProductoService>();
            builder.Services.AddScoped<ConfiteriaService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ReportesService>();
            builder.Services.AddScoped<SalidaService>();

            var app = builder.Build();

            // Migraciones y configuración SQLite
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
                db.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");
                db.Database.ExecuteSqlRaw("PRAGMA synchronous=NORMAL;");
            }

            // Middleware global de errores
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // CORS debe ir antes de Authentication y Authorization
            app.UseCors("FrontendPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}