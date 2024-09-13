using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CIFinance.Infra.Repositorio;

internal class UsuarioRepositorio(BDContexto contexto) : IRepositorioEntidade<Usuario>
{
    private readonly BDContexto _bdContexto = contexto;
    public void Atualizar(Usuario entidade)
    {
        _bdContexto.Usuarios.Attach(entidade);
        //_bdContexto.Usuarios.Entry(entidade).State = EntityState.Modified;
        _bdContexto.Entry(entidade).State = EntityState.Modified;
        _bdContexto.Usuarios.Update(entidade);
    }

    public async Task CriarAsync(Usuario entidade)
    {
        await _bdContexto.Usuarios.AddAsync(entidade);
    }

    public void Excluir(Usuario entidade)
    {
        _bdContexto.Usuarios.Remove(entidade);
    }

    public async Task<Usuario?> ObterAsync(string idExterno)
    {
        return await _bdContexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdentificadorExterno == idExterno);
    }

    public async Task<Usuario?> ObterAsync(Expression<Func<Usuario, bool>> predicado)
    {
        return await _bdContexto.Usuarios.FirstOrDefaultAsync(predicate: predicado);
    }

    public async Task<ICollection<Usuario>?> ObterTodosAsync()
    {
        return await _bdContexto.Usuarios.AsNoTracking().ToListAsync();
    }
}
