namespace CIFinance.Dominio.Abstracoes;

public interface IRepositorioEntidade
{
    void Criar(IEntidade entidade);
    void Atualizar(IEntidade entidade);
    void Excluir(IEntidade entidade);
    IEntidade? Obter(IEntidade entidade);
}
