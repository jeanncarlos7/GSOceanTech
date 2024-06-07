using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanTech.Domain.Entities;
using OceanTech.Domain.Interfaces.Repositories;

namespace OceanTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontuacaoController : Controller
    {
        private readonly IPontuacaoRepository _pontuacaoRepository;
        private readonly IPontuacaoDiariaRepository _pontuacaoDiariaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PontuacaoController(IPontuacaoRepository iPontuacao, IPontuacaoDiariaRepository iPontuacaoDiaria, IUsuarioRepository iUsuario)
        {
            _pontuacaoRepository = iPontuacao;
            _pontuacaoDiariaRepository = iPontuacaoDiaria;
            _usuarioRepository = iUsuario;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pontuacao>>> GetPontuacaos()
        {
            try
            {
                var pontuacoes = await _pontuacaoRepository.GetUPontuacoesAsync();

                return Ok(pontuacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pontuacao>> GetPontuacao(int id)
        {
            try
            {
                var pontuacao = await _pontuacaoRepository.GetPontuacaoByIdAsync(id);

                if (pontuacao == null)
                    return BadRequest("Pontuação  não existe ou foi deletado.");

                return Ok(pontuacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pontuacao>> PostPontuacaoDiaria(Pontuacao pontuacao)
        {
            try
            {
                if (pontuacao == null)
                    return NotFound();

                pontuacao.PontuacaoMensal = await _pontuacaoDiariaRepository.GetTotalPontuacaoAsync(pontuacao.UsuarioId);
                pontuacao.Usuario = await _usuarioRepository.GetUsuarioByIdAsync(pontuacao.UsuarioId);

                if (pontuacao.Usuario == null)
                    return BadRequest("Usuario não encontrado!");


                if (pontuacao.PontuacaoMensal == 0)
                    return BadRequest("Pontuação insuficiente!");

                await _pontuacaoRepository.AddPontuacaoAsync(pontuacao);

                return Ok(pontuacao.PontuacaoMensal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPontuacao(int id, Pontuacao pontuacao)
        {
            try
            {
                var existingPontuacao = await _pontuacaoRepository.GetPontuacaoByIdAsync(id);
                var usuario = await _usuarioRepository.GetUsuarioByIdAsync(pontuacao.UsuarioId);


                if (id != pontuacao.Id)
                    return BadRequest("Pontução errado.");

                if (existingPontuacao == null)
                    return NotFound("Pontuação não encontrada.");

                if (usuario == null)
                    return NotFound("Usuário não encontrado.");



                existingPontuacao.Usuario = usuario;
                existingPontuacao.PontuacaoMensal = pontuacao.PontuacaoMensal;
                //existingPontuacao.Usuario = usuario;


                await _pontuacaoRepository.UpdatePontuacaoAsync(existingPontuacao);

                return Ok(pontuacao);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePontuacao(int id)
        {
            await _pontuacaoRepository.DeletePontuacaoAsync(id);
            return NoContent();
        }
    }
}