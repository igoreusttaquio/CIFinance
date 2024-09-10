using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Repositorio;

public class TransacaoRepositorio(BDContexto bdContexto) : IRepositorioEntidade<Transacao>
{
    private readonly BDContexto _bancoDados = bdContexto;
    public async Task AtualizarAsync(Transacao entidade)
    {
        _bancoDados.Transacoes.Attach(entidade);
        _bancoDados.Transacoes.Entry(entidade).State = EntityState.Modified;
        _bancoDados.Transacoes.Update(entidade);
        await SalvarAsync();
    }

    public async Task CriarAsync(Transacao entidade)
    {
        _bancoDados.Transacoes.Add(entidade);
        await SalvarAsync();
    }

    public async Task ExcluirAsync(Transacao entidade)
    {
        _bancoDados.Transacoes.Remove(entidade);
        await SalvarAsync();
    }

    public async Task<Transacao?> ObterAsync(string idExterno)
    {
        return await _bancoDados.Transacoes.AsNoTracking().FirstOrDefaultAsync(t => t.IdentificadorExterno == idExterno);
    }

    public async Task<ICollection<Transacao>?> ObterTodosAsync()
    {
        return await _bancoDados.Transacoes.AsNoTracking().ToListAsync();
    }

    public async Task SalvarAsync()
    {
        await _bancoDados.SaveChangesAsync();
    }
}
