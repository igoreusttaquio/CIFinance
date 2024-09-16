using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using CIFinance.Infra.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CIFinance.Infra;

public static class InjecaoDependencia
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection servicos, string stringConexao)
    {

        servicos.AddScoped<InterceptadorSoftDelete>();

        servicos.AddDbContext<DbContext, BDContexto>((serviceProvider, options) =>
            options.UseInMemoryDatabase("TestDatabase")  // UseSqlServer(connectionString) para producao
                   .AddInterceptors(serviceProvider.GetRequiredService<InterceptadorSoftDelete>()));

        servicos.AddScoped(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));
        servicos.AddScoped<IRepositorioUsuario, UsuarioRepositorio>();
        servicos.AddScoped<IUnidadeTrabalho, UnidadeTrabalho>();

        return servicos;
    }
}
