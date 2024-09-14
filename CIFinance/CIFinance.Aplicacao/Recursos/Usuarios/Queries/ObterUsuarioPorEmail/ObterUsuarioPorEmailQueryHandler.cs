using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Queries.UbterUsuarioPorEmail;

public class ObterUsuarioPorEmailQueryHandler(IRepositorioUsuario repositorio) : IRequestHandler<ObterUsuarioPorEmailQuery, Resultado<UsuarioModel, Erro>>
{
    private readonly IRepositorioUsuario _repositorio = repositorio;
    public async Task<Resultado<UsuarioModel, Erro>> Handle(ObterUsuarioPorEmailQuery request, CancellationToken cancellationToken)
    {
        if (await _repositorio.ObterPorEmailAsync(request.Email) is Usuario usuario)
        {
            UsuarioModel um = usuario;
            return um;
        }
        return UsuarioErros.UsuarioNaoEncontrado;
    }
}
