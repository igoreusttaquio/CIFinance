using CIFinance.Aplicacao.Abstracoes;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CIFinance.Aplicacao.Servicos;

public class SenhaServico : IServicoSenha
{
    public string GerarHashSenha(string senha, out byte[] saltoSenha)
    {
        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = ComputarHash(salt, senha);
        saltoSenha = salt;
        return hashed;
    }

    public string ComputarHash(byte[] saltoSenha, string senha)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: senha!,
            salt: saltoSenha,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}