using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Dominio.Abstracoes;

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
