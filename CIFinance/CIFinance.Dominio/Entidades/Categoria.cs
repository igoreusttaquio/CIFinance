using CIFinance.Dominio.Enumeradores;
using CIFinance.Dominio.Excecoes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIFinance.Dominio.Entidades;

[Table("Categorias")]
public class Categoria : Entidade
{
    [Required]
    [MaxLength(50)]
    public string Nome { get; private set; } = null!;

    [Required]
    [MaxLength(50)]
    [AllowedValues(nameof(Enumeradores.Tipo.Receita), nameof(Enumeradores.Tipo.Receita))]
    public string Tipo { get; private set; } = null!;
    public Usuario Usuario { get; private set; } = null!;

    protected Categoria() { } // Requerido por causa do entity framework
    public Categoria(string nome, Tipo tipo, Usuario usuario)
    {
        ArgumentException.ThrowIfNullOrEmpty(nome, nameof(nome));
        Nome = nome;
        Tipo = tipo.ToString();
        Usuario = usuario;
    }

    public override void Atualizar<TEntidade>(TEntidade entidade)
    {
        if (entidade is Categoria categoria)
        {
            Nome = categoria.Nome;
            Tipo = categoria.Tipo;
        }
        else
            throw new EntidadeInvalidaExcecao("Entidade especificada nao e do tipo Categoria", nameof(entidade));
    }
}
