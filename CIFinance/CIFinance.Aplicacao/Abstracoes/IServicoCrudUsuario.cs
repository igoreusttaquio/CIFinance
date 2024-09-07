using CIFinance.Aplicacao.Servicos;

namespace CIFinance.Aplicacao.Abstracoes;

public interface IServicoCrudUsuario<TDto>
{
    Task<ServicoResposta<bool>> Criar(TDto dto);
    Task<ServicoResposta<IEnumerable<TDto?>>> ObterTodos();
    Task<ServicoResposta<TDto?>> Obter(string identificadorExterno);
    Task<ServicoResposta<bool>> Atualizar(TDto dto, string identificadorExterno);
    Task<ServicoResposta<bool>> Remover(string identificadorExterno);
}
