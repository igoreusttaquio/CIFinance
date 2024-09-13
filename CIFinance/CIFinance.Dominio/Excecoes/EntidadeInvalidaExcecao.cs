namespace CIFinance.Dominio.Excecoes;

public class EntidadeInvalidaExcecao : ArgumentException
{
    public EntidadeInvalidaExcecao(string mensagem) : base(mensagem) { }
    public EntidadeInvalidaExcecao(string mensagem, string parametro) : base(mensagem, parametro) { }
    public EntidadeInvalidaExcecao(string mensagem, string parametro, Exception interna) : base(mensagem, parametro, interna) { }
}
