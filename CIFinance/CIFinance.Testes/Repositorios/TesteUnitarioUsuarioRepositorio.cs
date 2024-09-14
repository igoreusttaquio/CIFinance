
using CIFinance.Infra.Dados;
using Microsoft.EntityFrameworkCore;
using CIFinance.Infra.Repositorio;
using CIFinance.Dominio.Entidades;

namespace CIFinance.Testes.Repositorios;

public class TesteUnitarioUsuarioRepositorio
{
    private readonly UsuarioRepositorio _usuarioRepositorio;
    private readonly DbContextOptions<BDContexto> _options;
    private readonly string _idUsuario;
    private readonly BDContexto _contexto;

    private readonly string _nomeUsuario = "Igor Silva";
    private readonly string _emailUsuario = "igoreusttaquio@gmail.com";
    private readonly string _outroNome = "Silva Igor";
    private readonly string _outroEmail = "silvaigor@gmail.com";

    public TesteUnitarioUsuarioRepositorio()
    {
        // arrange
        _options = new DbContextOptionsBuilder<BDContexto>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        _contexto = new BDContexto(_options);
        var usuario = Usuario.Fabrica.Criar(_nomeUsuario, _emailUsuario);
        _contexto.Usuarios.Add(usuario);
        _contexto.SaveChanges();

        _idUsuario = usuario.IdentificadorExterno;

        _usuarioRepositorio = new UsuarioRepositorio(_contexto);
    }

    [Fact]
    public async void ObterUsuarioPorEmail_RetornaUsuario()
    {
        // act
        var usuario = await _usuarioRepositorio.ObterPorEmailAsync(_emailUsuario);

        // assert
        Assert.NotNull(usuario);
    }

    [Fact]
    public async void ObterUsuarioPorEmail_RetornaUsuarioComNomeCorreto()
    {
        // act
        var usuario = await _usuarioRepositorio.ObterPorEmailAsync(_emailUsuario);

        // assert
        Assert.NotNull(usuario);
        Assert.Equal(_nomeUsuario, usuario.Nome);
    }

    [Fact]
    public async void ObterUsuarioPorId_RetornaUsuario()
    {
        // act 
        var usuario = await _usuarioRepositorio.ObterPorIdAsync(_idUsuario);

        // assert
        Assert.NotNull(usuario);
    }

    [Fact]
    public async void ObterUsuarioPorId_RetornaUsuarioComNomeCorreto()
    {
        // act 
        var usuario = await _usuarioRepositorio.ObterPorIdAsync(_idUsuario);

        // assert
        Assert.NotNull(usuario);
        Assert.Equal(_nomeUsuario, usuario.Nome);
    }

    [Fact]
    public async void SalvarUsuario_RetornaUsuarioComNomeAlterado()
    {
        // act 
        Usuario? usuario;
        if (await _usuarioRepositorio.ObterPorIdAsync(_idUsuario) is Usuario u)
        {
            u.Atualizar(Usuario.Fabrica.Criar(_outroNome, _emailUsuario));
            _usuarioRepositorio.Atualizar(u);
            _contexto.SaveChanges();
        }

        usuario = await _usuarioRepositorio.ObterPorIdAsync(_idUsuario);

        // assert
        Assert.NotNull(usuario);
        Assert.Equal(_outroNome, usuario.Nome);
    }

    [Fact]
    public async void CriarUsuario_RetornaUsuario()
    {
        // act 
        var usuario = Usuario.Fabrica.Criar(_outroNome, _outroEmail);

        await _usuarioRepositorio.CriarAsync(usuario);
        await _contexto.SaveChangesAsync();

        // assert
        Assert.NotNull(await _usuarioRepositorio.ObterPorIdAsync(usuario.IdentificadorExterno));
        Assert.Equal(_outroNome, usuario.Nome);
    }

    [Fact]
    public async void ExcluirUsuario_RetornaVerdadeiro()
    {
        // act 
        var usuario = await _usuarioRepositorio.ObterPorIdAsync(_idUsuario);
        bool registoExcluido = false;

        if(usuario is not null)
        {
            _usuarioRepositorio.Excluir(usuario);
            registoExcluido = await _contexto.SaveChangesAsync() > 0;
        }

        // assert
        Assert.Null(await _usuarioRepositorio.ObterPorIdAsync(_idUsuario));
        Assert.True(registoExcluido);
    }

    [Fact]
    public async void ObterTodosUsuario_RetornaListaUsuarios()
    {
        // act 
        var usuarios = await _usuarioRepositorio.ObterTodosAsync();


        // assert
        Assert.NotNull(usuarios);
        Assert.True(usuarios is not null and IEnumerable<Usuario>);
    }

}