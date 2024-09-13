using System.Drawing;

namespace CIFinance.Dominio.Abstracoes;

public class Resultado<Tvalor, TErro>
{
    public readonly Tvalor? Valor;
    public readonly TErro? Erro;

    private bool _sucesso;

    private Resultado(Tvalor valor)
    {
        _sucesso = true;
        Valor = valor;
        Erro = default;
    }

    private Resultado(TErro erro)
    {
        _sucesso = false;
        Valor = default;
        Erro = erro;
    }

    // Caminho feliz
    public static implicit operator Resultado<Tvalor, TErro>(Tvalor valor) => new(valor);

    // Caminho do erro
    public static implicit operator Resultado<Tvalor, TErro>(TErro erro) => new(erro);


    public Resultado<Tvalor, TErro> Match(Func<Tvalor, Resultado<Tvalor, TErro>> sucesso, Func<TErro, Resultado<Tvalor, TErro>> falha)
    {
        if (_sucesso)
        {
            return sucesso(Valor!);
        }
        return falha(Erro!);
    }

}
