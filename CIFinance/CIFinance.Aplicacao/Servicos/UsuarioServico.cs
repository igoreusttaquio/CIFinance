using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Aplicacao.Dtos.Usuario;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;

namespace CIFinance.Aplicacao.Servicos;

internal class UsuarioServico(IServicoSenha servicoSenha, IRepositorioEntidade<Usuario> repositorio) : IServicoCrudUsuario<UsuarioDTO>, IServicoUsuario
{

    public IServicoSenha ServicoSenha => servicoSenha;
    private readonly IRepositorioEntidade<Usuario> _repositorioUsuario = repositorio;

    public async Task<ServicoResposta<bool>> Atualizar(UsuarioDTO dto, string uidUsuario)
    {
        var resposta = new ServicoResposta<bool>(true);
        if (dto?.IdentificadorExterno is null)
        {
            return resposta;
        }

        try
        {
            if (await _repositorioUsuario.ObterAsync(dto.IdentificadorExterno) is Usuario usuario && uidUsuario == usuario.IdentificadorExterno)
            {
                Usuario novo;
                if (dto.HashSenha is not null && dto.SaltoSenha is not null)
                {
                    novo = new Usuario(dto.Nome, dto.Email, dto.HashSenha, dto.SaltoSenha);
                }
                else
                {
                    novo = new Usuario(dto.Nome, dto.Email);
                }

                usuario.Atualizar(novo);
                await _repositorioUsuario.AtualizarAsync(usuario);

                resposta.ComSucesso();
            }
            else
                resposta.ComErro("Usuario nao encontrado");
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }

        return resposta;
    }

    public async Task<ServicoResposta<bool>> Criar(UsuarioDTO dto)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(dto?.Senha);
        ArgumentException.ThrowIfNullOrEmpty(dto?.Senha);

        var resposta = new ServicoResposta<bool>(true);

        try
        {
            var hash = ServicoSenha.GerarHashSenha(dto.Senha, out byte[] saltoSenha);
            var novoUsuario = new Usuario(dto.Nome, dto.Email, hash, saltoSenha);
            await _repositorioUsuario.CriarAsync(novoUsuario);

            resposta.ComSucesso();
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }
        return resposta;
    }

    public async Task<ServicoResposta<UsuarioDTO?>> Obter(string uidExterno)
    {
        var resposta = new ServicoResposta<UsuarioDTO?>(null);
        try
        {
            if (await _repositorioUsuario.ObterAsync(uidExterno) is Usuario usuario)
            {
                var dto = new UsuarioDTO
                {
                    Email = usuario.Email,
                    IdentificadorExterno = usuario.IdentificadorExterno,
                    Nome = usuario.Nome
                };

                resposta.ComSucesso(dto);
            }

        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }

        return resposta;
    }

    public async Task<ServicoResposta<IEnumerable<UsuarioDTO?>>> ObterTodos()
    {
        var resposta = new ServicoResposta<IEnumerable<UsuarioDTO?>>([]);
        try
        {
            if (await _repositorioUsuario.ObterTodosAsync() is IEnumerable<Usuario> usuarios)
            {
                var mapeados = usuarios.ToList()
                    .ConvertAll(u =>
                    new UsuarioDTO { Email = u.Email, Nome = u.Nome, IdentificadorExterno = u.IdentificadorExterno });
                resposta.ComSucesso(mapeados);
            }

        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }

        return resposta;
    }

    public async Task<ServicoResposta<bool>> Remover(string uidUsuario)
    {
        var resposta = new ServicoResposta<bool>(true);
        try
        {
            if (await _repositorioUsuario.ObterAsync(uidUsuario) is Usuario usuario)
            {
                await _repositorioUsuario.ExcluirAsync(usuario);
                resposta.ComSucesso();
            }
            else
                resposta.ComErro("Usuario nao encontrado");
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }

        return resposta;
    }

    public bool ValidarSenha(string senhaFornecida, string hashSenhaUsuario, byte[] saltoSenha)
    {
        return hashSenhaUsuario.Equals(ServicoSenha.ComputarHash(saltoSenha, senhaFornecida));
    }
}
