using CIFinance.Dominio.Excecoes;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIFinance.Dominio.Entidades;

[Table("Orcamentos")]
public class Orcamento : Entidade
{
    public Usuario Usuario { get; private set; } = null!;
    public Categoria Categoria { get; private set; } = null!;
    public decimal Valor { get; private set; }
    public DateTime DataInicio { get; private set; } = DateTime.UtcNow;
    public DateTime DataFim { get; private set; } = DateTime.UtcNow;
    public string? Descricao { get; private set; }

    public Orcamento() { }
    public Orcamento(Usuario usuario, Categoria categoria, decimal valor, DateTime dataInicio, DateTime dataFim, string? descricao = null)
    {
        if (dataInicio > DateTime.Today)
        {
            throw new DataInvalidaExcecao("Data informada inválida.");
        }

        if (dataFim > dataInicio)
        {
            throw new DataInvalidaExcecao("A data de início não pode ser menor que a data de fim.");
        }

        Usuario = usuario;
        Categoria = categoria;
        Valor = valor;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Descricao = descricao;
    }
    public override void Atualizar<TEntidade>(TEntidade entidade)
    {
        if (entidade is Orcamento orcamento)
        {
            Categoria = orcamento.Categoria;
            Valor = orcamento.Valor;
            DataInicio = orcamento.DataInicio;
            DataFim = orcamento.DataFim;
            Descricao = orcamento.Descricao;
        }
        else
            throw new EntidadeInvalidaExcecao("Entidade especificada nao e do tipo Orcamento", nameof(entidade));
    }
}
