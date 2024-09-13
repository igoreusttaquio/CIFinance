using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CIFinance.Infra.Repositorio;

public class CategoriaRepositorio(BDContexto bdContexto) : IRepositorioEntidade<Categoria>
{
    private readonly BDContexto _bancoDados = bdContexto;
    public void Atualizar(Categoria entidade)
    {
        _bancoDados.Categorias.Attach(entidade);
        _bancoDados.Entry(entidade).State = EntityState.Modified;
        _bancoDados.Categorias.Update(entidade);
    }

    public async Task CriarAsync(Categoria entidade)
    {
        await _bancoDados.Categorias.AddAsync(entidade);
    }

    public void Excluir(Categoria entidade)
    {
        _bancoDados.Categorias.Remove(entidade);
    }

    public async Task<Categoria?> ObterAsync(string idExterno)
    {
        return await _bancoDados.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.IdentificadorExterno == idExterno);
    }

    public async Task<Categoria?> ObterAsync(Expression<Func<Categoria, bool>> predicado)
    {
        return await _bancoDados.Categorias.AsNoTracking().FirstOrDefaultAsync(predicate: predicado);
    }

    public async Task<ICollection<Categoria>?> ObterTodosAsync()
    {
        return await _bancoDados.Categorias.AsNoTracking().ToListAsync();
    }
}
