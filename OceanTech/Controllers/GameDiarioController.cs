using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanTech.Domain.Entities;
using OceanTech.Domain.Interfaces.Repositories;

namespace OceanTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDiarioController : Controller
    {
        private readonly IGameDiarioRepository _gameDiarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public GameDiarioController(IGameDiarioRepository gameDiarioRepository, IUsuarioRepository usuarioRepository)
        {
            _gameDiarioRepository = gameDiarioRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDiario>>> GetGameDiario()
        {
            try
            {
                var gameDiarios = await _gameDiarioRepository.GetUGameDiariosAsync();
                return Ok(gameDiarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDiario>> GetGameDiario(int id)
        {
            try
            {
                var gameDiario = await _gameDiarioRepository.GetGameDiarioByIdAsync(id);

                if (gameDiario == null)
                    return BadRequest("GameDiario não existe ou foi deletado.");

                return Ok(gameDiario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GameDiario>> PostGameDiario(GameDiario gameDiario)
        {
            try
            {
                if (gameDiario == null)
                    return NotFound();

                if (gameDiario.Jogou == true)
                    return BadRequest("Já jogou hoje.");

                if (!gameDiario.UsuarioId.HasValue || _usuarioRepository.GetUsuarioByIdAsync(gameDiario.UsuarioId.Value) == null)
                    return BadRequest("Usuário não encontrado.");



                await _gameDiarioRepository.AddGameDiarioAsync(gameDiario);

                return Ok(gameDiario);
            }
            catch (DbUpdateException dbEx)
            {
                var innerExceptionMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                return BadRequest($"Erro ao salvar as mudanças: {innerExceptionMessage}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameDiario(int id, GameDiario gameDiario)
        {
            try
            {
                if (id != gameDiario.Id)
                    return BadRequest("GameDiario errado.");

                var usuario = await _usuarioRepository.GetUsuarioByIdAsync(gameDiario.UsuarioId.Value);
                if (!gameDiario.UsuarioId.HasValue || usuario == null)
                    return BadRequest("Usuário não encontrado.");

                await _gameDiarioRepository.UpdateGameDiarioAsync(gameDiario);

                return Ok(gameDiario);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameDiario(int id)
        {
            await _gameDiarioRepository.DeleteGameDiarioAsync(id);
            return NoContent();
        }
    }
}