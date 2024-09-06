using CIFinance.Dominio.Excecoes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIFinance.Dominio.Entidades;

[Table("Contas")]
public class Conta : Entidade
{
    [Required]
    [MaxLength(100)]
    public string Nome { get; private set; } = null!;
    public decimal Saldo { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    protected Conta() { }// Requerido por causa do entity framework
    public Conta(string nome, decimal saldo, Usuario)
    {
        ArgumentException.ThrowIfNullOrEmpty(nome, nameof(nome));
        Nome = nome;
        Saldo = saldo;
        Usuario = Usuario;
    }
    public override void Atualizar<TEntidade>(TEntidade entidade)
    {
        if (entidade is Conta conta)
        {
            Nome = conta.Nome;
            Saldo = conta.Saldo;
        }

        throw new NotImplementedException();
    }

    public void Depositar(decimal valor)
    {
        Saldo += valor;
    }

    public void Sacar(decimal valor)
    {
        if (valor > Saldo)
        {
            throw new ExcecaoSaque("Valor insuficiente para saque.");
        }

        Saldo -= valor;
    }
}
