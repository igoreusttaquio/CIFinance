namespace CIFinance.Dominio.Abstracoes;

public sealed record ConfiguracaoToken(string ChaveSecreta, string Emissor, string Audiencia, long Timestamp);
