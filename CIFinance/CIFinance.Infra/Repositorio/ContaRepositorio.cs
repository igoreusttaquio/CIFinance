using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Repositorio;

public class ContaRepositorio(BDContexto BancoDeDados) : IRepositorioEntidade<Conta>
{
    private readonly BDContexto bDContexto = BancoDeDados;

    public async Task AtualizarAsync(Conta entidade)
    {
        bDContexto.Contas.Attach(entidade);
        bDContexto.Entry(entidade).State = EntityState.Modified;
        bDContexto.Contas.Update(entidade);
        await SalvarAsync();
    }

    public async Task CriarAsync(Conta entidade)
    {
        await bDContexto.Contas.AddAsync(entidade);
        await SalvarAsync();
    }

    public async Task ExcluirAsync(Conta entidade)
    {
        bDContexto.Contas.Remove(entidade);
        await SalvarAsync();

    }

    public async Task<IEntidade?> ObterAsync(string idExterno)
    {
        return await bDContexto.Contas.AsNoTracking().FirstOrDefaultAsync(contas=> contas.IdentificadorExterno == idExterno);
    }

    public async Task<ICollection<Conta>?> ObterTodosAsync()
    {
        return await bDContexto.Contas.AsNoTracking().ToListAsync();
    }

    public async Task SalvarAsync()
    {
        await bDContexto.SaveChangesAsync();
    }
}
