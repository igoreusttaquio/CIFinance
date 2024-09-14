using CIFinance.Dominio.Entidades;

namespace CIFinance.Dominio.Abstracoes;

public interface IRepositorioUsuario : IRepositorioGenerico<Usuario>
{
    // coisas especificas do repositorio generico do usuario vem aqui
    Task<Usuario?> ObterPorEmailAsync(string email);
}
