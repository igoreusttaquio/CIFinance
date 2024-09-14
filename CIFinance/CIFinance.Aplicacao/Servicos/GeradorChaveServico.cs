using CIFinance.Aplicacao.Abstracoes;
using System.Security.Cryptography;

namespace CIFinance.Aplicacao.Servicos;

public class GeradorChaveServico : IGeradorChave
{
    public string GerarChaveBase64()
    {
        // chave de 256-bit (32 bytes)
        byte[] key = new byte[32];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }
        return Convert.ToBase64String(key);
    }
}