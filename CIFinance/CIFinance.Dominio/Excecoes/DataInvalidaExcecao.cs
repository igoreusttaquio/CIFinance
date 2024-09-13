namespace CIFinance.Dominio.Excecoes;

public class DataInvalidaExcecao : Exception
{
    public DataInvalidaExcecao() : base() { }
    public DataInvalidaExcecao(string mensagem) : base(mensagem) { }
    public DataInvalidaExcecao(string mensagem, Exception interna) : base(mensagem, interna) { }
}
