
using CIFinance.Aplicacao.Servicos;

namespace CIFinance.Aplicacao.Abstracoes;

public interface IServico<TDto>
{
    Task Criar(TDto dto, string uidUsuario);
    Task<ServicoResposta<IEnumerable<TDto>?>> ObterTodos(string uidUsuario);
    Task<ServicoResposta<TDto?>> Obter(string uidExterno, string uidUsuario);
    Task Atualizar(TDto dto, string uidUsuario);
    Task Remover(TDto dto);
}
