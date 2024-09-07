namespace CIFinance.Aplicacao.Servicos;

public class ServicoResposta<T>(T dados, bool sucesso = true, string? mensagem = null)
{
    public T Dados { get; private set; } = dados;
    public bool Sucesso { get; private set; } = sucesso;
    public string? Mensagem { get; private set; } = mensagem;

    public void ComErro(string mensagem)
    {
        Mensagem = mensagem;
        Sucesso = false;
    }

    public void ComSucesso(T dados, string? mensagem = null)
    {
        Dados = dados;
        ComSucesso(mensagem);
    }

    public void ComSucesso(string? mensagem = null)
    {
        Mensagem = mensagem;
        Sucesso = true;
    }
}
