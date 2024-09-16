using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.ObterUsuarioPorEmail;

public class ObterUsuarioPorEmailQueryHandler(IRepositorioUsuario repositorio) : IRequestHandler<ObterUsuarioPorEmailQuery, Resultado<UsuarioModel>>
{
    private readonly IRepositorioUsuario _repositorio = repositorio;
    public async Task<Resultado<UsuarioModel>> Handle(ObterUsuarioPorEmailQuery request, CancellationToken cancellationToken)
    {
        if (await _repositorio.ObterPorEmailAsync(request.Email) is Usuario usuario)
        {
            return (UsuarioModel)usuario;
        }
        return UsuarioErros.UsuarioNaoEncontrado;
    }
}
