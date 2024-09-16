using CIFinance.Aplicacao.Recursos.Usuarios;
using CIFinance.Aplicacao.Recursos.Usuarios.Comandos.CriarUsuario;
using CIFinance.Aplicacao.Recursos.Usuarios.Queries.ObterUsuarioPorEmail;
using CIFinance.WebAPI.Contratos.Usuario;
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
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioRequest usuario)
        {
            var comando = new CriarUsuarioComando(usuario.Nome, usuario.Email, usuario.Senha);
            var resultado = await _sender.Send(comando);

            return resultado.Exitou switch
            {
                true => Ok(resultado.Valor),
                _ => Problem(resultado?.Erro?.Mensagem)
            };
        }

        [HttpGet("por-email/{email}")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<UsuarioModel>> ObterUsuarioPorEmail(string email)
        {
            var resultado = await _sender.Send(new ObterUsuarioPorEmailQuery(email));
            if (resultado.Falhou)
            {
                return Problem(resultado?.Erro?.Mensagem, statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(new UsuarioResponse(resultado.Valor));
        }
    }
}
