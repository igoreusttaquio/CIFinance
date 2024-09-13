using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.UbterUsuarioPorEmail;

public class ObterUsuarioPorEmailQueryHandler(IRepositorioEntidade<Usuario> repositorio) : IRequestHandler<ObterUsuarioPorEmailQuery, Resultado<UsuarioModel, Erro>>
{
    private readonly IRepositorioEntidade<Usuario> _repositorio = repositorio;
    public async Task<Resultado<UsuarioModel, Erro>> Handle(ObterUsuarioPorEmailQuery request, CancellationToken cancellationToken)
    {
        if (await _repositorio.ObterAsync(u => u.Email == request.Email) is Usuario usuario)
        {
            UsuarioModel um = usuario;
            return um;
        }
        return UsuarioErros.UsuarioNaoEncontrado;
    }
}
