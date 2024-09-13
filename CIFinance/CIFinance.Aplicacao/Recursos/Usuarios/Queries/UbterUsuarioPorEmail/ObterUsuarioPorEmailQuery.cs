using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.UbterUsuarioPorEmail;

public record ObterUsuarioPorEmailQuery(string Email) : IRequest<UsuarioModel?>;
