
using Microsoft.EntityFrameworkCore;
using OceanTech.Database;
using OceanTech.Domain.Entities;
using OceanTech.Domain.Interfaces.Repositories;

namespace OceanTech.Repositories
{
    public class PontuacaoDiariaRepository : IPontuacaoDiariaRepository
    {
        private readonly OceanTechContext _context;

        public PontuacaoDiariaRepository(OceanTechContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PontuacaoDiaria>> GetUPontuacaoDiariasAsync()
        {
            return await _context.PontuacoesDiarias.ToListAsync();
        }

        public async Task<PontuacaoDiaria> GetPontuacaoDiariaByIdAsync(int id)
        {
            return await _context.PontuacoesDiarias.FindAsync(id);
        }

        public async Task AddPontuacaoDiariaAsync(PontuacaoDiaria pontuacaoDiaria)
        {
            _context.PontuacoesDiarias.Add(pontuacaoDiaria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePontuacaoDiariaAsync(PontuacaoDiaria pontuacaoDiaria)
        {
            _context.Entry(pontuacaoDiaria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePontuacaoDiariaAsync(int id)
        {
            var pontuacao = await _context.PontuacoesDiarias.FindAsync(id);
            if (pontuacao != null)
            {
                _context.PontuacoesDiarias.Remove(pontuacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalPontuacaoAsync(int id)
        {
            return await _context.PontuacoesDiarias
                .Join(
                    _context.GameDiarios,
                    pontuacaoDiaria => pontuacaoDiaria.GameDiarioId,
                    gameDiario => gameDiario.Id,
                    (pontuacaoDiaria, gameDiario) => new { pontuacaoDiaria, gameDiario }
                )
                .Join(
                    _context.Usuarios,
                    joined => joined.gameDiario.UsuarioId,
                    usuario => usuario.Id,
                    (joined, usuario) => new { joined.pontuacaoDiaria, usuario }
                )
                .Where(x => x.usuario.Id == id)
                .SumAsync(x => x.pontuacaoDiaria.Valor);
        }
    }
}
