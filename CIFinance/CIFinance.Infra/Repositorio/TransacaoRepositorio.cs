using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CIFinance.Infra.Repositorio;

public class TransacaoRepositorio(BDContexto bdContexto) : IRepositorioEntidade<Transacao>
{
    private readonly BDContexto _bancoDados = bdContexto;
    public void Atualizar(Transacao entidade)
    {
        _bancoDados.Transacoes.Attach(entidade);
        _bancoDados.Transacoes.Entry(entidade).State = EntityState.Modified;
        _bancoDados.Transacoes.Update(entidade);
    }

    public async Task CriarAsync(Transacao entidade)
    {
        _bancoDados.Transacoes.Add(entidade);
    }

    public void Excluir(Transacao entidade)
    {
        _bancoDados.Transacoes.Remove(entidade);
    }

    public async Task<Transacao?> ObterAsync(string idExterno)
    {
        return await _bancoDados.Transacoes.AsNoTracking().FirstOrDefaultAsync(t => t.IdentificadorExterno == idExterno);
    }

    public async Task<Transacao?> ObterAsync(Expression<Func<Transacao, bool>> predicado)
    {
        return await _bancoDados.Transacoes.AsNoTracking().FirstOrDefaultAsync(predicate: predicado);
    }

    public async Task<ICollection<Transacao>?> ObterTodosAsync()
    {
        return await _bancoDados.Transacoes.AsNoTracking().ToListAsync();
    }

}
