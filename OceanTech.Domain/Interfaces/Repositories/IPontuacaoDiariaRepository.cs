using OceanTech.Domain.Entities;

namespace OceanTech.Domain.Interfaces.Repositories
{
    public interface IPontuacaoDiariaRepository
    {
        Task<IEnumerable<PontuacaoDiaria>> GetUPontuacaoDiariasAsync();
        Task<PontuacaoDiaria> GetPontuacaoDiariaByIdAsync(int id);
        Task AddPontuacaoDiariaAsync(PontuacaoDiaria pontuacaoDiaria);
        Task UpdatePontuacaoDiariaAsync(PontuacaoDiaria pontuacaoDiaria);
        Task DeletePontuacaoDiariaAsync(int id);

        Task<int> GetTotalPontuacaoAsync(int id);
    }
}
