using CIFinance.Aplicacao;
using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Aplicacao.Servicos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CIFinance.Testes.Servicos;

public class TesteUnitarioServicoToken
{
    private readonly IToken _servicoToken;
    private readonly IConfiguracaoToken _configuracaoToken;
    private readonly string _userName = "igoreusttaquio@gmail.com";
    private readonly string _userId = "asty23wk";
    private readonly IGeradorChave _geradorChves;


    private readonly List<Claim> _claims;
    public TesteUnitarioServicoToken()
    {

        _geradorChves = new GeradorChaveServico();

        string base64Key = _geradorChves.GerarChaveBase64();

        _claims =
        [
            new (JwtRegisteredClaimNames.Sub, _userName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new ("userId", _userId)
        ];

        _servicoToken = new TokenServico();
        _configuracaoToken = new ConfiguracaoToken()
        {
            Audiencia = "testes.token",
            ChaveSecreta = base64Key,
            Emissor = "testes.token",
            Minutos = 2.2
        };
    }

    [Fact]
    public void VerificaGeracaoToken_RetornaVerdadero()
    {
        // act
        var token = _servicoToken.GerarToken(_configuracaoToken, [.. _claims]);

        // asert 
        Assert.NotNull(token);
    }

    [Fact]
    public void ValidacaoTokenGerado_RetornaVerdadeiro()
    {
        // act
        var token = _servicoToken.GerarToken(_configuracaoToken, [.. _claims]);

        // assert
        var claimsPrincial = _servicoToken.ValidarToken(token, _configuracaoToken);
        Assert.NotNull(claimsPrincial);
    }

    [Fact] 
    public void ValidarUsuarioIdTokenGerado_RetoronaVerdadeiro()
    {
        // act
        var token = _servicoToken.GerarToken(_configuracaoToken, [.. _claims]);

        // assert
        var claimsPrincial = _servicoToken.ValidarToken(token, _configuracaoToken);
        Assert.NotNull(claimsPrincial);
        Assert.Equal(claimsPrincial.Value.Identidades.FindFirst("userId")?.Value, _userId);
    }
}
