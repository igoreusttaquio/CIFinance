
using CIFinance.Aplicacao.Servicos;

namespace CIFinance.Aplicacao.Abstracoes;

public interface IServicoCrud<TDto>
{
    Task<ServicoResposta<bool>> Criar(TDto dto, string uidUsuario);
    Task<ServicoResposta<IEnumerable<TDto?>>> ObterTodos(string uidUsuario);
    Task<ServicoResposta<TDto?>> Obter(string uidExterno, string uidUsuario);
    Task<ServicoResposta<bool>> Atualizar(TDto dto, string uidUsuario);
    Task<ServicoResposta<bool>> Remover(string uidExterno, string uidUsuario);
}
