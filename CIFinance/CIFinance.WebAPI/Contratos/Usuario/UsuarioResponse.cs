using CIFinance.Aplicacao.Recursos.Usuarios;

namespace CIFinance.WebAPI.Contratos.Usuario;

public sealed record UsuarioResponse
{
    public string Nome { get; }
    public string Email { get; }
    public string Id { get; }

    public UsuarioResponse(UsuarioModel um)
    {
        Nome = um.Nome;
        Email = um.Email;
        Id = um.IdentificadorExterno;
    }
}