using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Repositorio;

public class ContaRepositorio(BDContexto dbContexto) : IRepositorioEntidade<Conta>
{
    private readonly BDContexto _bancoDados = dbContexto;

    public async Task AtualizarAsync(Conta entidade)
    {
        _bancoDados.Contas.Attach(entidade);
        _bancoDados.Entry(entidade).State = EntityState.Modified;
        _bancoDados.Contas.Update(entidade);
        await SalvarAsync();
    }

    public async Task CriarAsync(Conta entidade)
    {
        await _bancoDados.Contas.AddAsync(entidade);
        await SalvarAsync();
    }

    public async Task ExcluirAsync(Conta entidade)
    {
        _bancoDados.Contas.Remove(entidade);
        await SalvarAsync();

    }

    public async Task<Conta?> ObterAsync(string idExterno)
    {
        return await _bancoDados.Contas.AsNoTracking().FirstOrDefaultAsync(contas=> contas.IdentificadorExterno == idExterno);
    }

    public async Task<ICollection<Conta>?> ObterTodosAsync()
    {
        return await _bancoDados.Contas.AsNoTracking().ToListAsync();
    }

    public async Task SalvarAsync()
    {
        await _bancoDados.SaveChangesAsync();
    }
}
