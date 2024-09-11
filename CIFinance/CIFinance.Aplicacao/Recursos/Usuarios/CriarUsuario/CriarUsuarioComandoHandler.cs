using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.CriarUsuario;

// Cria um usuario e retorna o ID
public class CriarUsuarioComandoHandler(BDContexto dbContext, IServicoSenha servicoSenha) : IRequestHandler<CriarUsuarioComando, bool>
{
    private readonly BDContexto _bancoContexto = dbContext;
    private readonly IServicoSenha _servicoSenha = servicoSenha;

    public async Task<bool> Handle(CriarUsuarioComando request, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Senha);
        ArgumentException.ThrowIfNullOrEmpty(request.Senha);

        try
        {
            var hash = _servicoSenha.GerarHashSenha(request.Senha, out byte[] saltoSenha);
            var novoUsuario = new Usuario(request.Nome, request.Email, hash, saltoSenha);
            await _bancoContexto.Usuarios.AddAsync(novoUsuario, cancellationToken);
            await _bancoContexto.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            // logar exeption???
            return false;
        }
    }
}
