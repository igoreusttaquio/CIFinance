using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Repositorio;

public class CategoriaRepositorio(BDContexto bdContexto) : IRepositorioEntidade<Categoria>
{
    private readonly BDContexto _bancoDados = bdContexto;
    public async Task AtualizarAsync(Categoria entidade)
    {
        _bancoDados.Categorias.Attach(entidade);
        _bancoDados.Entry(entidade).State = EntityState.Modified;
        _bancoDados.Categorias.Update(entidade);
        await SalvarAsync();
    }

    public async Task CriarAsync(Categoria entidade)
    {
        await _bancoDados.Categorias.AddAsync(entidade);
        await SalvarAsync();
    }

    public async Task ExcluirAsync(Categoria entidade)
    {
        _bancoDados.Categorias.Remove(entidade);
        await SalvarAsync();
    }

    public async Task<IEntidade?> ObterAsync(string idExterno)
    {
        return await _bancoDados.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.IdentificadorExterno == idExterno);
    }

    public async Task<ICollection<Categoria>?> ObterTodosAsync()
    {
        return await _bancoDados.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task SalvarAsync()
    {
        await _bancoDados.SaveChangesAsync();
    }
}
