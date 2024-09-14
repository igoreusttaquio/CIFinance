using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
namespace CIFinance.Aplicacao.Abstracoes;

public interface IToken
{
    string GerarToken(IConfiguracaoToken configuracao, Claim[] claims);
    (ClaimsPrincipal Identidades, SecurityToken TokenValidado)? ValidarToken(string token, IConfiguracaoToken configuracao);
}
