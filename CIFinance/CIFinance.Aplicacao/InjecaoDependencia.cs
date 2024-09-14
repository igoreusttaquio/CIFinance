using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CIFinance.Aplicacao;

public static class InjecaoDependencia
{

    public static IServiceCollection AdicionarAplicacao(this IServiceCollection servicos, Assembly assembly)
    {
        servicos.AddMediatR(configuracoes =>
        {
            configuracoes.RegisterServicesFromAssembly(assembly);
        });
        return servicos;
    }
}
