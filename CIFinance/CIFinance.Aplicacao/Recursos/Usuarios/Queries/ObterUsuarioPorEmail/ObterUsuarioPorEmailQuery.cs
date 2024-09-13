using CIFinance.Dominio.Abstracoes;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.UbterUsuarioPorEmail;

public record ObterUsuarioPorEmailQuery(string Email) : IRequest<Resultado<UsuarioModel, Erro>>;
