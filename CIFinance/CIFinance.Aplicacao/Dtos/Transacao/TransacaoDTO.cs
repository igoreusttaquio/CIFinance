namespace CIFinance.Aplicacao.Dtos.Transacao;

public record TransacaoDTO(string IdConta, string IdCategoria, string Descricao, decimal Valor, string Tipo, DateTime? Data = null, string? IdentificadorExterno = null);
