using Microsoft.EntityFrameworkCore;
using OceanTech.Database;
using OceanTech.Domain.Interfaces.Repositories;
using OceanTech.Repositories;

namespace OceanTech
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            IServiceCollection serviceCollection = builder.Services.AddDbContext<OceanTechContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
               .EnableSensitiveDataLogging()
               .LogTo(Console.WriteLine)
            );


            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IGameDiarioRepository, GameDiarioRepository>();
            builder.Services.AddScoped<IPontuacaoRepository, PontuacaoRepository>();
            builder.Services.AddScoped<IPontuacaoDiariaRepository, PontuacaoDiariaRepository>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
