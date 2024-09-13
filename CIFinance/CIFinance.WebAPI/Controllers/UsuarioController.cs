using CIFinance.Aplicacao.Recursos.Usuarios;
using CIFinance.Aplicacao.Recursos.Usuarios.Comandos.CriarUsuario;
using CIFinance.Aplicacao.Recursos.Usuarios.Queries.UbterUsuarioPorEmail;
using MediatR;
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
            var resultado = await _sender.Send(comando);

            // se nao usar o implicit operator para fazer o cast... Seria um codigo tedioso
            // Match(resultadoValor => (Resultado<bool, Erro>)resultadoValor, erro => ((Resultado<bool, Erro>))erro);
            resultado.Match(resultadoValor => resultadoValor, erro => erro);
            if (resultado.Erro is not null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{email}")]
        [Route("obter-usuario/email")]
        public async Task<ActionResult<UsuarioModel>> ObterUsuarioPorEmail(string email)
        {
            var query = new ObterUsuarioPorEmailQuery(email);
            return Ok(await _sender.Send(query));
        }
    }
}
