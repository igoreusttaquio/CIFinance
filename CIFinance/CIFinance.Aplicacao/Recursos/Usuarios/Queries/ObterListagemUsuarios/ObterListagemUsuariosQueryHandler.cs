using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.ObterListagemUsuarios;

public class ObterListagemUsuariosQueryHandler(IRepositorioEntidade<Usuario> repositorioUsario) : IRequestHandler<ObterListagemUsuariosQuery, Resultado<ICollection<UsuarioModel>, Erro>>
{
    private readonly IRepositorioEntidade<Usuario> _repositorioUsuario = repositorioUsario;
    public async Task<Resultado<ICollection<UsuarioModel>, Erro>> Handle(ObterListagemUsuariosQuery request, CancellationToken cancellationToken)
    {
        if (await _repositorioUsuario.ObterTodosAsync() is ICollection<Usuario> usuarios)
        {
            return usuarios.ToList().ConvertAll(usuario => (UsuarioModel)usuario);
        }

        return UsuarioErros.NenhumUsuario;
    }
}
