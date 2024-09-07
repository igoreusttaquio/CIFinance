using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Repositorio;

internal class UsuarioRepositorio(BDContexto contexto) : IRepositorioEntidade<Usuario>
{
    private readonly BDContexto _bdContexto = contexto;
    public async Task AtualizarAsync(Usuario entidade)
    {
        _bdContexto.Usuarios.Attach(entidade);
        //_bdContexto.Usuarios.Entry(entidade).State = EntityState.Modified;
        _bdContexto.Entry(entidade).State = EntityState.Modified;
        _bdContexto.Usuarios.Update(entidade);
        await SalvarAsync();

    }

    public async Task CriarAsync(Usuario entidade)
    {
        await _bdContexto.Usuarios.AddAsync(entidade);
        await SalvarAsync();
    }

    public async Task ExcluirAsync(Usuario entidade)
    {
        _bdContexto.Usuarios.Remove(entidade);
        await SalvarAsync();
    }

    public async Task<IEntidade?> ObterAsync(string idExterno)
    {
        return await _bdContexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdentificadorExterno == idExterno);
    }

    public async Task<ICollection<Usuario>?> ObterTodosAsync()
    {
        return await _bdContexto.Usuarios.AsNoTracking().ToListAsync();
    }

    public async Task SalvarAsync()
    {
        await _bdContexto.SaveChangesAsync();
    }
}
