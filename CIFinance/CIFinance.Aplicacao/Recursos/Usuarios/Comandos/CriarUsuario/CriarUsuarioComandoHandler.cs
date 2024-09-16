using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Comandos.CriarUsuario;

// Cria um usuario e retorna o ID
public class CriarUsuarioComandoHandler(IRepositorioUsuario usuarioRepositorio, IServicoSenha servicoSenha,
    IUnidadeTrabalho unidadeTrabalho) : IRequestHandler<CriarUsuarioComando, Resultado<string>>
{
    private readonly IRepositorioUsuario _repositorioUsuario = usuarioRepositorio;
    private readonly IServicoSenha _servicoSenha = servicoSenha;
    private readonly IUnidadeTrabalho _unidadeTrabalho = unidadeTrabalho;

    public async Task<Resultado<string>> Handle(CriarUsuarioComando request, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Senha);
        ArgumentException.ThrowIfNullOrEmpty(request.Senha);

        try
        {
            var hash = _servicoSenha.GerarHashSenha(request.Senha, out byte[] saltoSenha);
            var usuario = Usuario.Fabrica.Criar(request.Nome, request.Email, hash, saltoSenha);
            await _repositorioUsuario.CriarAsync(usuario);
            await _unidadeTrabalho.SalvarAsync();

            return usuario.IdentificadorExterno;
        }
        catch
        {
            // logar exeption???
            return UsuarioErros.NaoFoiPossivelCriarUsuario;
        }
    }
}
