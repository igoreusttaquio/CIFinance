namespace CIFinance.Dominio.Abstracoes;

public interface IConfiguracaoToken
{
    string ChaveSecreta { get; }
    string Emissor { get; }
    string Audiencia { get; }
    double Minutos { get; }
}
