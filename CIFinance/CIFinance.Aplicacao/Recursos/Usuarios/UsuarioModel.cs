namespace CIFinance.Aplicacao.Recursos.Usuarios;

public record UsuarioModel
{
    public string Nome { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string IdentificadorExterno { get; set; } = null!;

    public static implicit operator UsuarioModel(Dominio.Entidades.Usuario usuario)
    {
        return new UsuarioModel
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            IdentificadorExterno = usuario.IdentificadorExterno
        };
    }
}
