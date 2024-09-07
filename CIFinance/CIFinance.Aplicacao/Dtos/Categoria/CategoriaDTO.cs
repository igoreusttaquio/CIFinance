using CIFinance.Aplicacao.Dtos.Usuario;

namespace CIFinance.Aplicacao.Dtos.Categoria;

public record CategoriaDTO
{
    public string Nome { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public UsuarioDTO? Usuario { get; set; }
    public string? IdentificadorExterno { get; set; }
}
