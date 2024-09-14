using System.Security.Claims;
namespace CIFinance.Dominio.Abstracoes;

public interface IToken
{
    string GerarToken(IConfiguracaoToken configuracao, Claim[] claims);
    ClaimsPrincipal? ValidarToken(string token, IConfiguracaoToken configuracao);
}
