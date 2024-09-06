using CIFinance.Dominio.Excecoes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIFinance.Dominio.Entidades;
[Table("Usuarios")]
public class Usuario : Entidade
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 3)]
    public string Nome { get; private set; } = string.Empty;
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; private set; } = string.Empty;

    [MaxLength(124)]
    public string HashSenha { get; private set; } = string.Empty;
    public byte[] SaltoSenha { get; private set; } = new byte[16];
    public DateTime UltimoLogin { get; private set; } = DateTime.UtcNow;

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

    public void AtualizarDataDeUltimoLogin()
    {
        UltimoLogin = DateTime.UtcNow;
    }
    public override void Atualizar<TEntidade>(TEntidade entidade)
    {

        if (entidade is Usuario usuario)
        {
            Nome = usuario.Nome;
            if (HashSenha != usuario.HashSenha)
            {
                HashSenha = usuario.HashSenha;
                SaltoSenha = usuario.SaltoSenha;
            }
        }
        else
            throw new EntidadeInvalidaExcecao("Entidade especificada nao e do tipo Usuario", nameof(entidade));
    }
}
