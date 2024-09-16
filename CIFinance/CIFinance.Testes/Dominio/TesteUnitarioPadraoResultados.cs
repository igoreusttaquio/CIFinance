using CIFinance.Dominio.Abstracoes;

namespace CIFinance.Testes.Dominio;

public class TesteUnitarioPadraoResultados
{
    [Fact]
    public void TestaPadraoResultadoDominio_RetornaVerdadeiro()
    {
        // arrange
        Resultado<string> resultado;

        // act
        var umaString = "conversaoImplicita";
        resultado = umaString;

        // assert
        Assert.True(resultado.Exitou);
        Assert.Equal(resultado.Valor, umaString);
        Assert.NotEqual(resultado.GetType(), umaString.GetType());
    }
}
