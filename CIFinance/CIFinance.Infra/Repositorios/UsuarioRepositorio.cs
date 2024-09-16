using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Infra.Repositorios;

public class UsuarioRepositorio(DbContext contexto) : RepositorioGenerico<Usuario>(contexto), IRepositorioUsuario
{
    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}
