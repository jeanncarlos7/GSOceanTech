using OceanTech.Domain.Entities;

namespace OceanTech.Domain.Interfaces.Repositories
{
    public interface IPontuacaoRepository
    {
        Task<IEnumerable<Pontuacao>> GetUPontuacoesAsync();
        Task<Pontuacao> GetPontuacaoByIdAsync(int id);
        Task AddPontuacaoAsync(Pontuacao pontuacao);
        Task UpdatePontuacaoAsync(Pontuacao pontuacao);
        Task DeletePontuacaoAsync(int id);
    }
}
