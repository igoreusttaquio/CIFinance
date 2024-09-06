using CIFinance.Dominio.Abstracoes;
using System.ComponentModel.DataAnnotations;

namespace CIFinance.Dominio.Entidades;

public abstract class Entidade : IEntidade
{
    public int Id { get; protected set; }
    public DateTime Criacao { get; protected set; } = DateTime.UtcNow;
    public DateTime UltimaAlteracao { get; protected set; } = DateTime.UtcNow;
    [MaxLength(10)]
    public string IdentificadorExterno { get; protected set; } = Identificador.Novo();
    public bool Ativo { get; protected set; } = true;

    public abstract void Atualizar<TEntidade>(TEntidade entidade);

    public void Inativar()
    {
        Ativo = false;
        AtualizarDataUltimaAlteracao();
    }

    public void AtualizarDataUltimaAlteracao()
    {
        UltimaAlteracao = DateTime.UtcNow;
    }
}
