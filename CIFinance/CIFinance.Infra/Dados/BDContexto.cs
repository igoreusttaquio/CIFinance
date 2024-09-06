using CIFinance.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Dados;

public class BDContexto : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasQueryFilter(c => c.Ativo);
    }
}
