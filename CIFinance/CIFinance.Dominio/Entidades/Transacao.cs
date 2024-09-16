using CIFinance.Dominio.Enumeradores;
using CIFinance.Dominio.Excecoes;
using CIFinance.Dominio.Extensoes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIFinance.Dominio.Entidades;
[Table("Transacoes")]
public class Transacao : Entidade
{
    public Conta Conta { get; private set; } = null!;
    public Categoria Categoria { get; private set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Descricao { get; private set; } = null!;

    [Required]
    [DataType(DataType.Currency)]
    public decimal Valor { get; private set; } = 0m;

    [Required]
    public DateTime Data { get; private set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(50)]
    [AllowedValues(nameof(Enumeradores.Tipo.Receita), nameof(Enumeradores.Tipo.Despesa))]
    public string Tipo { get; private set; } = null!;

    protected Transacao() { } // Requerido por causa do entity framework

    public Transacao(Conta conta, Categoria categoria, string descricao, decimal valor, Tipo tipo, DateTime? data = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(descricao, nameof(descricao));
        if (categoria.Tipo.ParaEnum<Tipo>() != tipo)
        {
            var msg = $"Nao e possivel utilizar um tipo de transaceo inconsistente ao tipo da categoria. Informado: {tipo}, Categoria {categoria.Tipo}.";
            throw new ArgumentException(msg, nameof(tipo));
        }

        Valor = valor;
        Conta = conta;
        Tipo = tipo.ToString();
        Categoria = categoria;
        Descricao = descricao;
        Data = data is null ? DateTime.UtcNow : (DateTime)data;
    }

    public override void Atualizar<TEntidade>(TEntidade entidade)
    {
        if (entidade is Transacao transacao)
        {
            Valor = transacao.Valor;
            Conta = transacao.Conta;
            Tipo = transacao.Tipo;
            Categoria = transacao.Categoria;
            Descricao = transacao.Descricao;
            Data = transacao.Data;
        }
        else
            throw new EntidadeInvalidaExcecao("Entidade especificada nao e do tipo Transacao", nameof(entidade));
    }
}
