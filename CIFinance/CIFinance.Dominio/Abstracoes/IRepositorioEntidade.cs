namespace CIFinance.Dominio.Abstracoes;

public interface IRepositorioEntidade<TEntidade>
{
    Task CriarAsync(TEntidade entidade);
    Task AtualizarAsync(TEntidade entidade);
    Task ExcluirAsync(TEntidade entidade);
    Task<IEntidade?> ObterAsync(string idExterno);
    Task<ICollection<TEntidade>?> ObterTodosAsync();
    Task SalvarAsync();
}
