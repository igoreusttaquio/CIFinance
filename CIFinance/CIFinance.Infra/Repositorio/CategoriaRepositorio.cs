using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Repositorio;

public class CategoriaRepositorio(BDContexto bdContexto) : IRepositorioEntidade<Categoria>
{
    private readonly BDContexto _bancoDados = bdContexto;
    public async Task Atualizar(Categoria entidade)
    {
        _bancoDados.Categorias.Update(entidade);
        await _bancoDados.SaveChangesAsync();
    }

    public async Task Criar(Categoria entidade)
    {
        await _bancoDados.Categorias.AddAsync(entidade);
        await _bancoDados.SaveChangesAsync();
    }

    public async Task Excluir(Categoria entidade)
    {
        _bancoDados.Categorias.Remove(entidade);
        await _bancoDados.SaveChangesAsync();
    }

    public async Task<IEntidade?> Obter(string idExterno)
    {
        return await _bancoDados.Categorias.FirstOrDefaultAsync(c => c.IdentificadorExterno == idExterno);
    }

    public async Task<ICollection<Categoria>?> Obter()
    {
        return await _bancoDados.Categorias.ToListAsync();
    }
}
