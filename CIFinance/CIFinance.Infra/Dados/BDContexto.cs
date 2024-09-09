using CIFinance.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Dados;

public class BDContexto : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Conta> Contas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasQueryFilter(c => c.Ativo);
        modelBuilder.Entity<Usuario>().HasQueryFilter(u => u.Ativo);
        modelBuilder.Entity<Conta>().HasQueryFilter(c => c.Ativo);
    }
}
