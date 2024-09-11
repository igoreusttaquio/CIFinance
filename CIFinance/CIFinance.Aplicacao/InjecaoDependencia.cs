using Microsoft.Extensions.DependencyInjection;

namespace CIFinance.Aplicacao;

public static class InjecaoDependencia
{
    public static IServiceCollection AdicionarAplicacao(this IServiceCollection servicos)
    {
        servicos.AddMediatR(configuracoes =>
        {
            configuracoes.RegisterServicesFromAssembly(typeof(InjecaoDependencia).Assembly);// talvez precise ser a Program...
        });
        return servicos;
    }

    public static IServiceCollection AdicionarAplicacao(this IServiceCollection servicos, Type tipo)
    {
        servicos.AddMediatR(configuracoes =>
        {
            configuracoes.RegisterServicesFromAssembly(tipo.Assembly);
        });
        return servicos;
    }
}
