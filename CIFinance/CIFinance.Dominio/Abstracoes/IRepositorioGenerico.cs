namespace CIFinance.Dominio.Abstracoes;

public interface IRepositorioGenerico<T> where T : class, IEntidade
{
    Task<IEnumerable<T>?> ObterTodosAsync();
    Task<T?> ObterPorIdAsync(string identificadorExterno);
    Task CriarAsync(T entidate);
    void Atualizar(T entidate);
    void Excluir(T entidade);
}
