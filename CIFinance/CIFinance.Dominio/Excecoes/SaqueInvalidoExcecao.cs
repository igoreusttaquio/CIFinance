namespace CIFinance.Dominio.Excecoes;

public class SaqueInvalidoExcecao : Exception
{
    public SaqueInvalidoExcecao() : base() { }
    public SaqueInvalidoExcecao(string message) : base(message) { }
    public SaqueInvalidoExcecao(string message, Exception interna) : base(message, interna) { }
}
