namespace CIFinance.Dominio.Abstracoes;

public interface IRepositorioEntidade<TEntidade>
{
    Task CriarAsync(TEntidade entidade);
    Task AtualizarAsync(TEntidade entidade);
    Task ExcluirAsync(TEntidade entidade);
    Task<TEntidade?> ObterAsync(string idExterno);
    Task<ICollection<TEntidade>?> ObterTodosAsync();
    Task SalvarAsync();
}
