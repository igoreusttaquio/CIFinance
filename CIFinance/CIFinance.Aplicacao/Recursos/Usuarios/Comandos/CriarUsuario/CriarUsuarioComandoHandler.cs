using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Comandos.CriarUsuario;

// Cria um usuario e retorna o ID
public class CriarUsuarioComandoHandler(IRepositorioEntidade<Usuario> usuarioRepositorio, IServicoSenha servicoSenha,
    IUnidadeTrabalho unidadeTrabalho) : IRequestHandler<CriarUsuarioComando, Resultado<bool, Erro>>
{
    private readonly IRepositorioEntidade<Usuario> _repositorioUsuario = usuarioRepositorio;
    private readonly IServicoSenha _servicoSenha = servicoSenha;
    private readonly IUnidadeTrabalho _unidadeTrabalho = unidadeTrabalho;

    public async Task<Resultado<bool, Erro>> Handle(CriarUsuarioComando request, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Senha);
        ArgumentException.ThrowIfNullOrEmpty(request.Senha);

        try
        {
            var hash = _servicoSenha.GerarHashSenha(request.Senha, out byte[] saltoSenha);
            await _repositorioUsuario.CriarAsync(Usuario.Fabrica.Criar(request.Nome, request.Email, hash, saltoSenha));
            await _unidadeTrabalho.SalvarAsync();

            return (Resultado<bool, Erro>)true;
        }
        catch
        {
            // logar exeption???
            return (Resultado<bool, Erro>)UsuarioErros.NaoFoiPossivelCriarUsuario;
        }
    }
}
