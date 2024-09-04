using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIFinance.Dominio.Entidades;
[Table("Usuarios")]
public class Usuario : Padrao
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 3)]
    public string Nome { get; private set; } = string.Empty;
    [EmailAddress]
    [Required]
    [MaxLength(100)]
    public string Email { get; private set; } = string.Empty;

    [MaxLength(124)]
    public string HashSenha { get; private set; } = string.Empty;
    public byte[] SaltoSenha { get; private set; } = new byte[16];
    public DateTime UltimoLogin { get; private set; } = DateTime.Now;

    protected Usuario() { } // requerido pelo entity framework

    public Usuario(string nome, string email, string hashSenha, byte[] saltoSenha) : this(nome, email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(hashSenha, nameof(hashSenha));
        if (saltoSenha.Length == 0) throw new ArgumentException(null, nameof(saltoSenha));

        HashSenha = hashSenha;
        SaltoSenha = saltoSenha;
    }

    public Usuario(string nome, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nome, nameof(nome));
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

        Nome = nome;
        Email = email;
    }
    public override void Atualizar<TEntidade>(TEntidade entidade)
    {
        throw new NotImplementedException();
    }
}
