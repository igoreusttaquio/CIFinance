namespace CIFinance.Aplicacao.Abstracoes;

public interface IServicoUsuario
{
    IServicoSenha ServicoSenha { get; }
    IToken ServicoToken { get; }
    public bool ValidarSenha(string senhaFornecida, string hashSenhaUsuario, byte[] saltoSenha);
}
