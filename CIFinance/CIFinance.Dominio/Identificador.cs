using Sqids;

namespace CIFinance.Dominio;

public static class Identificador
{
    private static readonly SqidsOptions opcoes = new()
    {
        Alphabet = "k3G7QAe51FCsPW92uEOyq4Bg6Sp8YzVTmnU0liwDdHXLajZrfxNhobJIRcMvKt",
    };

    private static readonly SqidsEncoder<int> sqids = new(opcoes);

    private static int NumeroAleatorio => new Random().Next(byte.MinValue, byte.MaxValue);

    public static string Novo()
    {
        int timestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        int limite = 10;
        var identificador = sqids.Encode(NumeroAleatorio, NumeroAleatorio, timestamp);
        if(identificador.Length > limite)
        {
            return identificador[..limite];
        }
        return identificador;
    }
}
