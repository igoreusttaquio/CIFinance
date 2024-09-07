namespace CIFinance.Aplicacao.Abstracoes;

public interface IServicoSenha
{
    public string GerarHashSenha(string password, out byte[] passwordSalt);
    public string ComputarHash(byte[] salt, string password);
}
