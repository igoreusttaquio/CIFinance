using CIFinance.Dominio.Abstracoes;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Comandos.CriarUsuario;
/// <summary>
/// Retorna o ID do usuario criado
/// </summary>
/// <param name="Nome"></param>
/// <param name="Email"></param>
/// <param name="Senha"></param>
public record CriarUsuarioComando(string Nome, string Email, string Senha) : IRequest<Resultado<bool, Erro>>;