using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CIFinance.Dominio.Abstracoes;

namespace CIFinance.Infra.Dados;

internal class InterceptadorSoftDelete : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(
                eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<IEntidade>> entries =
            eventData
                .Context
                .ChangeTracker
                .Entries<IEntidade>()
                .Where(e => e.State == EntityState.Deleted);

        foreach (EntityEntry<IEntidade> softDeletavel in entries)
        {
            softDeletavel.State = EntityState.Modified;
            softDeletavel.Entity.Inativar();
            softDeletavel.Entity.AtualizarDataUltimaAlteracao();
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
