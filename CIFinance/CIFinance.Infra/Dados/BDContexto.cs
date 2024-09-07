using CIFinance.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Dados;

public class BDContexto : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasQueryFilter(c => c.Ativo);
        modelBuilder.Entity<Usuario>().HasQueryFilter(u => u.Ativo);
    }
}
