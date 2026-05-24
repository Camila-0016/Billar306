using Billar306.API.Configuration;
using Billar306.API.Data;
using Billar306.API.Middleware;
using Billar306.API.Repositories;
using Billar306.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Billar306.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //CORS 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:3000",   // React / Node
                            "http://localhost:5500",   // Live Server de VS Code
                            "http://127.0.0.1:5500"   // Live Server alternativo
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
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IMesaRepository, MesaRepository>();
            builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
            builder.Services.AddScoped<ISesionMesaRepository, SesionMesaRepository>();
            builder.Services.AddScoped<IConfiguracionRepository, ConfiguracionRepository>();
            builder.Services.AddScoped<IFiadoRepository, FiadoRepository>();
            builder.Services.AddScoped<IAnticipoRepository, AnticipoRepository>();
            builder.Services.AddScoped<IClienteFrecuenteRepository, ClienteFrecuenteRepository>();
            builder.Services.AddScoped<IItemConfiteriaRepository, ItemConfiteriaRepository>();
            builder.Services.AddScoped<IEventoTurnoRepository, EventoTurnoRepository>();
            builder.Services.AddScoped<IIngresoStockRepository, IngresoStockRepository>();
            builder.Services.AddScoped<IRegistroHoraRepository, RegistroHoraRepository>();

            // Services
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<MesaService>();
            builder.Services.AddScoped<TurnoService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<FiadoService>();
            builder.Services.AddScoped<AnticipoService>();
            builder.Services.AddScoped<ClienteFrecuenteService>();
            builder.Services.AddScoped<ConfiteriaService>();
            builder.Services.AddScoped<EventoService>();
            builder.Services.AddScoped<LiquidacionService>();
            builder.Services.AddScoped<IVentaDirectaRepository, VentaDirectaRepository>();
            builder.Services.AddScoped<IAbonoFiadoRepository, AbonoFiadoRepository>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Ingresá el token así: Bearer {tu token}"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

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