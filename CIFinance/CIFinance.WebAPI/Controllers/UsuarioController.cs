using CIFinance.Aplicacao.Dtos.Usuario;
using CIFinance.Aplicacao.Recursos.Usuarios.CriarUsuario;
using CIFinance.Aplicacao.Recursos.Usuarios.UbterUsuarioPorEmail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIFinance.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        [Route("criar-usuario")]
        public async Task<IActionResult> CriarUsuario(CriarUsuarioComando comando)
        {
            return await _sender.Send(comando) ? Ok(comando) : BadRequest(comando);
        }

        [HttpGet("{email}")]
        [Route("obter-usuario/email")]
        public async Task<ActionResult<UsuarioDTO>> ObterUsuarioPorEmail(string email)
        {
            var query = new ObterUsuarioPorEmailQuery(email);
            return Ok(await _sender.Send(query));
        }
    }
}
