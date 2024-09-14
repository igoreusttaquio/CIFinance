using CIFinance.Dominio.Abstracoes;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Repositorio;

public class RepositorioGenerico<T>(DbContext contexto) : IRepositorioGenerico<T> where T : class, IEntidade
{
    protected readonly DbSet<T> _dbSet = contexto.Set<T>();

    public void Atualizar(T entidate)
    {
        _dbSet.Update(entidate);
    }

    public async Task CriarAsync(T entidate)
    {
        await _dbSet.AddAsync(entidate);
    }

    public void Excluir(T entidade)
    {
        _dbSet.Remove(entidade);
    }

    public async Task<T?> ObterPorIdAsync(string identificadorExterno)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.IdentificadorExterno == identificadorExterno);
    }

    public async Task<IEnumerable<T>?> ObterTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }
}
