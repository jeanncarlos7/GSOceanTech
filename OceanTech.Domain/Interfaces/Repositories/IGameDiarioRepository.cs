using OceanTech.Domain.Entities;


namespace OceanTech.Domain.Interfaces.Repositories
{
    public interface IGameDiarioRepository
    {
        Task<IEnumerable<GameDiario>> GetUGameDiariosAsync();
        Task<GameDiario> GetGameDiarioByIdAsync(int id);
        Task AddGameDiarioAsync(GameDiario gameDiario);
        Task<GameDiario> ExisteRegistroAsync(int usuario, DateTime date);
        Task UpdateGameDiarioAsync(GameDiario gameDiario);
        Task DeleteGameDiarioAsync(int id);
    }
}
