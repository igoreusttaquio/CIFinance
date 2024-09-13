using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CIFinance.Infra.Repositorio;

public class ContaRepositorio(BDContexto dbContexto) : IRepositorioEntidade<Conta>
{
    private readonly BDContexto _bancoDados = dbContexto;

    public void Atualizar(Conta entidade)
    {
        _bancoDados.Contas.Attach(entidade);
        _bancoDados.Entry(entidade).State = EntityState.Modified;
        _bancoDados.Contas.Update(entidade);
    }

    public async Task CriarAsync(Conta entidade)
    {
        await _bancoDados.Contas.AddAsync(entidade);
    }

    public void Excluir(Conta entidade)
    {
        _bancoDados.Contas.Remove(entidade);
    }

    public async Task<Conta?> ObterAsync(string idExterno)
    {
        return await _bancoDados.Contas.AsNoTracking().FirstOrDefaultAsync(contas => contas.IdentificadorExterno == idExterno);
    }

    public async Task<Conta?> ObterAsync(Expression<Func<Conta, bool>> predicado)
    {
        return await _bancoDados.Contas.AsNoTracking().FirstOrDefaultAsync(predicate: predicado);
    }

    public async Task<ICollection<Conta>?> ObterTodosAsync()
    {
        return await _bancoDados.Contas.AsNoTracking().ToListAsync();
    }
}
