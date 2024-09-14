using CIFinance.Aplicacao.Abstracoes;

namespace CIFinance.Aplicacao.Servicos;

public class UsuarioServico : IServicoUsuario
{
    public IServicoSenha ServicoSenha => throw new NotImplementedException();

    public IToken ServicoToken => throw new NotImplementedException();

    public bool ValidarSenha(string senhaFornecida, string hashSenhaUsuario, byte[] saltoSenha)
    {
        throw new NotImplementedException();
    }
}
