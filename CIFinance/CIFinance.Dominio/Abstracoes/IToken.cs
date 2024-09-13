using System.Security.Claims;
namespace CIFinance.Dominio.Abstracoes;

public interface IToken
{
    string GerarToken(ConfiguracaoToken configuracao, Claim[] claims);
    ClaimsPrincipal? ValidateToken(string token, ConfiguracaoToken configuracao);
}
