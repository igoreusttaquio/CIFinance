using CIFinance.Dominio.Excecoes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIFinance.Dominio.Entidades;

[Table("Contas")]
public class Conta : Padrao
{
    [MaxLength(100)]
    public string Nome { get; private set; } = string.Empty;
    public decimal Saldo { get; private set; }
    public Usuario? Usuario { get; private set; }

    protected Conta() { }// Requerido por causa do entity framework
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
