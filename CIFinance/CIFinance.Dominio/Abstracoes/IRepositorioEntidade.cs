using System.Linq.Expressions;

namespace CIFinance.Dominio.Abstracoes;

public interface IRepositorioEntidade<TEntidade>
{
    Task CriarAsync(TEntidade entidade);
    void Atualizar(TEntidade entidade);
    void Excluir(TEntidade entidade);
    Task<TEntidade?> ObterAsync(string idExterno);
    Task<TEntidade?> ObterAsync(Expression<Func<TEntidade, bool>> predicado);
    Task<ICollection<TEntidade>?> ObterTodosAsync();
}
