namespace CIFinance.Dominio.Extensoes;

public static class Enumeradores
{
    public static TEnum ParaEnum<TEnum>(this string @string) where TEnum : struct, Enum
    {
        return Enum.Parse<TEnum>(@string);
    }
}
