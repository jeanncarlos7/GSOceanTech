using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OceanTech.Database;
using OceanTech.Domain.Entities;
using OceanTech.Domain.Interfaces.Repositories;
namespace OceanTech.Repositories
{
    public class PontuacaoRepository : IPontuacaoRepository
    {
        private readonly OceanTechContext _context;

        public PontuacaoRepository(OceanTechContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pontuacao>> GetUPontuacoesAsync()
        {
            return await _context.Pontuacoes.ToListAsync();
        }

        public async Task<Pontuacao> GetPontuacaoByIdAsync(int id)
        {
            return await _context.Pontuacoes.FindAsync(id);
        }

        public async Task AddPontuacaoAsync(Pontuacao pontuacao)
        {
            _context.Pontuacoes.Add(pontuacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePontuacaoAsync(Pontuacao pontuacao)
        {
            _context.Entry(pontuacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePontuacaoAsync(int id)
        {
            var pontuacao = await _context.Pontuacoes.FindAsync(id);
            if (pontuacao != null)
            {
                _context.Pontuacoes.Remove(pontuacao);
                await _context.SaveChangesAsync();
            }
        }
    }
}
