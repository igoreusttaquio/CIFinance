using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Aplicacao.Recursos.Usuarios.Comandos.CriarUsuario;
using CIFinance.Aplicacao.Servicos;
using CIFinance.Dominio.Abstracoes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CIFinance.Aplicacao;

public static class InjecaoDependencia
{

    public static IServiceCollection AdicionarAplicacao(this IServiceCollection servicos)
    {
        servicos.AddSingleton<IServicoSenha, SenhaServico>();
        servicos.AddScoped<IRequestHandler<CriarUsuarioComando, Resultado<string>>, CriarUsuarioComandoHandler>(); 

        servicos.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IMarcadorAssemblyAplicacao).Assembly));

        return servicos;
    }
}
