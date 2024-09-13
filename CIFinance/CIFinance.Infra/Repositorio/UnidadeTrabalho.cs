using CIFinance.Dominio.Abstracoes;
using CIFinance.Infra.Dados;

namespace CIFinance.Infra.Repositorio;

public class UnidadeTrabalho(BDContexto bDContexto) : IUnidadeTrabalho
{
    private readonly BDContexto _bdContexto = bDContexto;
    public async Task SalvarAsync()
    {
        await _bdContexto.SaveChangesAsync();
    }
}
