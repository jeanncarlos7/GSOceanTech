using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanTech.Database;
using OceanTech.Domain.Entities;
using OceanTech.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OceanTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontuacaoDiariaController : Controller
    {
        private readonly IPontuacaoDiariaRepository _pontuacaoDiariaRepository;
        private readonly IGameDiarioRepository _gameDiarioRepository;

        public PontuacaoDiariaController(IPontuacaoDiariaRepository iPontuacaoDiaria, IGameDiarioRepository iGameDiario)
        {
            _pontuacaoDiariaRepository = iPontuacaoDiaria;
            _gameDiarioRepository = iGameDiario;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontuacaoDiaria>>> GetPontuacaoDiarias()
        {
            try
            {
                var pontuacaoDiarias = await _pontuacaoDiariaRepository.GetUPontuacaoDiariasAsync();
                return Ok(pontuacaoDiarias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PontuacaoDiaria>> GetPontuacaoDiaria(int id)
        {
            try
            {
                var pontuacaoDiaria = await _pontuacaoDiariaRepository.GetPontuacaoDiariaByIdAsync(id);

                if (pontuacaoDiaria == null)
                    return BadRequest("Pontuação Diaria não existe ou foi deletado.");

                return Ok(pontuacaoDiaria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PontuacaoDiaria>> PostPontuacaoDiaria(PontuacaoDiaria pontuacaoDiaria)
        {
            try
            {
                if (pontuacaoDiaria == null)
                    return NotFound();

                var gameDiario = await _gameDiarioRepository.GetGameDiarioByIdAsync(pontuacaoDiaria.GameDiarioId);
                if (pontuacaoDiaria.GameDiarioId == 0 || gameDiario == null)
                    return BadRequest("Usuário não encontrado.");

                pontuacaoDiaria.GameDiario = gameDiario;

                await _pontuacaoDiariaRepository.AddPontuacaoDiariaAsync(pontuacaoDiaria);

                return Ok(pontuacaoDiaria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPontuacaoDiaria(int id, PontuacaoDiaria pontuacaoDiaria)
        {
            try
            {
                if (id != pontuacaoDiaria.Id)
                    return BadRequest("Pontuação Diaria errado.");

                var gameDiario = await _gameDiarioRepository.GetGameDiarioByIdAsync(pontuacaoDiaria.GameDiarioId);
                if (pontuacaoDiaria.GameDiarioId == 0 || gameDiario == null)
                    return BadRequest("Usuário não encontrado.");

                await _pontuacaoDiariaRepository.UpdatePontuacaoDiariaAsync(pontuacaoDiaria);

                return Ok(pontuacaoDiaria);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePontuacaoDiaria(int id)
        {
            await _pontuacaoDiariaRepository.DeletePontuacaoDiariaAsync(id);
            return NoContent();
        }
    }
}