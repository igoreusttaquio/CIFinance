﻿namespace CIFinance.Dominio.Abstracoes;

public sealed record ConfiguracaoToken : IConfiguracaoToken
{
    public string ChaveSecreta { get; init; } = null!;
    public string Emissor { get; init; } = null!;
    public string Audiencia { get; init; } = null!;
    public double Minutos { get; init; }
}
