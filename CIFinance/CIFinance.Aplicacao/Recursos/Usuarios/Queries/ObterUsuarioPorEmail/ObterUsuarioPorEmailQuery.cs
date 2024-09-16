using CIFinance.Dominio.Abstracoes;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.ObterUsuarioPorEmail;

public record ObterUsuarioPorEmailQuery(string Email) : IRequest<Resultado<UsuarioModel>>;
