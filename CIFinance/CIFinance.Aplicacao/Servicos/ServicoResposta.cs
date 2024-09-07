namespace CIFinance.Aplicacao.Servicos;

public class ServicoResposta<T>
{
    public T Dados { get; private set; }
    public bool Sucesso { get; private set; } = true;
    public string? Mensagem { get; private set; }

    public void ComErro(string mensagem)
    {
        Mensagem = mensagem;
        Sucesso = false;
    }

    public void ComSucesso(T dados, string? mensagem = null)
    {
        Dados = dados;
        Mensagem = mensagem;
    }
}
