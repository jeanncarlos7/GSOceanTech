using Microsoft.EntityFrameworkCore;
using OceanTech.Database.Mapping;
using OceanTech.Domain.Entities;


namespace OceanTech.Database
{
    public class OceanTechContext : DbContext
    {
        public OceanTechContext(DbContextOptions<OceanTechContext> options) : base(options) { }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<GameDiario> GameDiarios { get; set; }
        public DbSet<Pontuacao> Pontuacoes { get; set; }
        public DbSet<PontuacaoDiaria> PontuacoesDiarias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new PontuacaoMapping());
            modelBuilder.ApplyConfiguration(new GameDiarioMapping());
            modelBuilder.ApplyConfiguration(new PontuacaoDiariaMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}