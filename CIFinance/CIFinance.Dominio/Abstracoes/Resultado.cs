namespace CIFinance.Dominio.Abstracoes;

public class Resultado<T>
{
    private readonly T? _valor;
    public bool Exitou { get; }
    public bool Falhou => !Exitou;

    public Erro? Erro { get; }

    public T Valor
    {
        get
        {
            if (Falhou)
                throw new InvalidOperationException("Valor invalido para o estado atual do objeto");

            return _valor!;
        }

        init { _valor = value; }

    }

    private Resultado(Erro erro)
    {
        Exitou = false;
        Erro = erro;
    }

    private Resultado(T valor)
    {
        Valor = valor;
        Erro = null;
        Exitou = true;
    }

    public static implicit operator Resultado<T>(T valor) => new(valor);
    public static implicit operator Resultado<T>(Erro erro) => new(erro);

    public static Resultado<T> Sucesso(T valor) => new(valor);
    public static Resultado<T> Falha(Erro erro) => new(erro);

}
