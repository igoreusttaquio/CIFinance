using CIFinance.Dominio.Excecoes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIFinance.Dominio.Entidades;
[Table("Transferencias")]
public class Transferencia : Entidade
{
    public Usuario Usuario { get; private set; } = null!;
    public Categoria Categoria { get; private set; } = null!;
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; } = DateTime.UtcNow;
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Descricao { get; private set; } = null!;
    public Transferencia() { }

    public Transferencia(Usuario usuario, Categoria categoria, decimal valor, DateTime data, string descricao)
    {
        ArgumentException.ThrowIfNullOrEmpty(descricao);
        if (data < DateTime.Today)
        {
            throw new ArgumentException("Data infomada inválida.");
        }

        if (valor < 0)
        {
            throw new ArgumentException("Valor informado inválido.");
        }

        Usuario = usuario;
        Categoria = categoria;
        Valor = valor;
        Data = data;
        Descricao = descricao;
    }

    public override void Atualizar<TEntidade>(TEntidade entidade)
    {
        if (entidade is Transferencia transferencia)
        {
            Categoria = transferencia.Categoria;
            Valor = transferencia.Valor;
            Data = transferencia.Data;
            Descricao = transferencia.Descricao;
        }
        else
            throw new EntidadeInvalidaExcecao("Entidade especificada nao e do tipo Transferencia", nameof(entidade));
    }
}
