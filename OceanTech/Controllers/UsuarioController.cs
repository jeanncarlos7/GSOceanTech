using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanTech.Domain.Entities;
using OceanTech.Domain.Interfaces.Repositories;
using OceanTech.HttpObjects;

namespace OceanTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);

                if (usuario == null)
                    return BadRequest("Usuario não existe ou foi deletado.");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioRequest usuarioRequest)
        {
            var usuario = new Usuario
            {
                Ativo = usuarioRequest.Ativo,
                Email = usuarioRequest.Email,
                Nome = usuarioRequest.Nome,
                Senha = usuarioRequest.Senha,
                Inscricao = DateTime.Now
            };

            try
            {
                
                if (!usuario.Ativo)
                    return BadRequest("Usuário desativado");

                await _usuarioRepository.AddUsuarioAsync(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {

            try
            {
                if (id != usuario.Id)
                    return BadRequest("Usuario errado.");

                if (usuario.Id == 0)
                    return BadRequest("Id vazio");

                if (usuario.Inscricao != DateTime.MinValue)
                    return BadRequest("Data não pode ser alterado.");


                await _usuarioRepository.UpdateUsuarioAsync(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuarioRepository.DeleteUsuarioAsync(id);
            return NoContent();
        }
    }
}