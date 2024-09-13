using CIFinance.Dominio.Abstracoes;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CIFinance.Aplicacao.Servicos;

public class TokenServico : IToken
{
    public string GerarToken(ConfiguracaoToken configuracao, Claim[] claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var chave = Convert.FromBase64String(configuracao.ChaveSecreta);

        var descritorToken = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuracao.Timestamp / 60),
            Audience = configuracao.Audiencia,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature),

        };

        var token = tokenHandler.CreateToken(descritorToken);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token, ConfiguracaoToken configuracao)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(configuracao.ChaveSecreta);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuracao.Emissor,
                ValidAudience = configuracao.Audiencia,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return principal;
        }
        catch
        {
            return null;
        }
    }
}
