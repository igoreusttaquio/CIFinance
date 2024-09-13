namespace CIFinance.Dominio.Abstracoes;

public sealed record Erro(string Codigo, string? Mensagem = null);
