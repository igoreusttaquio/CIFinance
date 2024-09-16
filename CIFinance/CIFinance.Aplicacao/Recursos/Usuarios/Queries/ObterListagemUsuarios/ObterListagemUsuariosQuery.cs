using CIFinance.Dominio.Abstracoes;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.ObterListagemUsuarios;

public class ObterListagemUsuariosQuery : IRequest<Resultado<ICollection<UsuarioModel>>>;
