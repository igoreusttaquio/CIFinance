namespace CIFinance.Dominio.Abstracoes;

public interface IRepositorioEntidade<TEntidade>
{
    Task Criar(TEntidade entidade);
    Task Atualizar(TEntidade entidade);
    Task Excluir(TEntidade entidade);
    Task<IEntidade?> Obter(string idExterno);
    Task<ICollection<TEntidade>?> ObterTodos();
    Task Salvar();
}
