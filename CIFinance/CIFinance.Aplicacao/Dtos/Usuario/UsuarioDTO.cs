
namespace CIFinance.Aplicacao.Dtos.Usuario;

public class UsuarioDTO
{
    public string Nome { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string? IdentificadorExterno { get; set; }
    public byte[]? SaltoSenha { get; set; }
    public string? HashSenha { get; set; }
    public string? Senha { get; set; }

}
