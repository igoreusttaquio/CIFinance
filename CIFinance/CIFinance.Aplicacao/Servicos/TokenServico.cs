using CIFinance.Aplicacao.Abstracoes;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CIFinance.Aplicacao.Servicos;

public class TokenServico : IToken
{
    public string GerarToken(IConfiguracaoToken configuracao, Claim[] claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var chave = Convert.FromBase64String(configuracao.ChaveSecreta);

        var descritorToken = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuracao.Minutos),
            Audience = configuracao.Audiencia,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuracao.Emissor
        };

        var token = tokenHandler.CreateToken(descritorToken);
        return tokenHandler.WriteToken(token);
    }

    public (ClaimsPrincipal Identidades, SecurityToken TokenValidado)? ValidarToken(string token, IConfiguracaoToken configuracao)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] chaveBytes = Convert.FromBase64String(configuracao.ChaveSecreta);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuracao.Emissor,
            ValidAudience = configuracao.Audiencia,
            IssuerSigningKey = new SymmetricSecurityKey(chaveBytes),
            ClockSkew = TimeSpan.Zero // Opicional: Setar para zero previne problemas de clock skew 
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken tokenValidado);

            // verificacoes adicionais podem vir aqui

            return (principal, tokenValidado);
        }
        catch (SecurityTokenExpiredException)
        {
            // gerenciar expiracao de token
            return null;
        }
        catch (SecurityTokenInvalidSignatureException)
        {
            // Gerenciar assinatura invalida do token
            return null;
        }
        catch (Exception ex)
        {
            // logar exceptions
            return null;
        }
    }

}
