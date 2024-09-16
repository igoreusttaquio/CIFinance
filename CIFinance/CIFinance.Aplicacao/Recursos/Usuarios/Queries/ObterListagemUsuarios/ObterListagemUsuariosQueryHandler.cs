using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.ObterListagemUsuarios;

public class ObterListagemUsuariosQueryHandler(IRepositorioUsuario repositorioUsario) : IRequestHandler<ObterListagemUsuariosQuery, Resultado<ICollection<UsuarioModel>>>
{
    private readonly IRepositorioUsuario _repositorioUsuario = repositorioUsario;
    public async Task<Resultado<ICollection<UsuarioModel>>> Handle(ObterListagemUsuariosQuery request, CancellationToken cancellationToken)
    {
        if (await _repositorioUsuario.ObterTodosAsync() is ICollection<Usuario> usuarios)
        {
            return usuarios.ToList().ConvertAll(usuario => (UsuarioModel)usuario);
        }

        return UsuarioErros.NenhumUsuario;
    }
}
