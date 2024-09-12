using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Comandos.AlterarSenhaUsuario;

public record AlterarSenhaUsuarioComando(string IdentificadorExterno, string SenhaAntiga, string SenhaNova) : IRequest<bool>
{
}
