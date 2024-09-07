using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using CIFinance.Infra.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace CIFinance.Infra;

public static class InjecaoDependencia
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection servicos, string stringConexao)
    {
        servicos.AddSingleton<InterceptadorSoftDelete>();
        
        servicos.AddScoped<IRepositorioEntidade<Categoria>, CategoriaRepositorio>();
        servicos.AddScoped<IRepositorioEntidade<Usuario>, UsuarioRepositorio>();

        servicos.AddDbContext<BDContexto>(
        (sp, options) => options
        //.UseSqlServer(stringConexao)
        .AddInterceptors(
            sp.GetRequiredService<InterceptadorSoftDelete>()));

        return servicos;
    }
}
