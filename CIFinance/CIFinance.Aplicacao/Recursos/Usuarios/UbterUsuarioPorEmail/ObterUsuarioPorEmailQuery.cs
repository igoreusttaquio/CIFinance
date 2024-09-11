using CIFinance.Aplicacao.Dtos.Usuario;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.UbterUsuarioPorEmail;

public record ObterUsuarioPorEmailQuery(string Email) : IRequest<UsuarioDTO?>;
