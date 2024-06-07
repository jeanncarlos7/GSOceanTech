using Microsoft.EntityFrameworkCore;
using OceanTech.Database;
using OceanTech.Domain.Entities;
using OceanTech.Domain.Interfaces.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OceanTech.Repositories
{
    public class GameDiarioRepository : IGameDiarioRepository
    {
        private readonly OceanTechContext _context;

        public GameDiarioRepository(OceanTechContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameDiario>> GetUGameDiariosAsync()
        {
            return await _context.GameDiarios.ToListAsync();
        }

        public async Task<GameDiario> GetGameDiarioByIdAsync(int id)
        {
            return await _context.GameDiarios.FindAsync(id);
        }

        public async Task AddGameDiarioAsync(GameDiario gameDiario)
        {   
            _context.GameDiarios.Add(gameDiario);
            await _context.SaveChangesAsync();
        }

        public async Task<GameDiario> ExisteRegistroAsync(int usuario, DateTime date)
        {
            return await _context.GameDiarios
                .FirstOrDefaultAsync(x => x.UsuarioId == usuario && x.dataJogo.Date == date.Date);
        }

        public async Task UpdateGameDiarioAsync(GameDiario gameDiario)
        {
            _context.Entry(gameDiario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameDiarioAsync(int id)
        {
            var gameDiario = await _context.GameDiarios.FindAsync(id);
            if (gameDiario != null)
            {
                _context.GameDiarios.Remove(gameDiario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
