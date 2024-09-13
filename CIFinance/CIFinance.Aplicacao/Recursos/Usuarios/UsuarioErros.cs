using CIFinance.Dominio.Abstracoes;

namespace CIFinance.Aplicacao.Recursos.Usuarios;

public static class UsuarioErros
{
    public static readonly Erro SenhaIvalida = new("UsuarioErros.SenhaInvalida", "Senha invalida");
    public static readonly Erro SenhasNaoCombinam = new("UsuarioErros.SenhasNaoCombinam", "As senhas fornecidas nao combinam");
    public static readonly Erro UsuarioNaoEncontrado = new("UsuarioErros.NaoEncontrado", "O Usuario nao foi encontrado");
    public static readonly Erro NaoFoiPossivelCriarUsuario = new("UsuarioErros.NaoFoiPossivelCriarUsuario", "Houve um problema ao tentar criar um novo usuario");
}
