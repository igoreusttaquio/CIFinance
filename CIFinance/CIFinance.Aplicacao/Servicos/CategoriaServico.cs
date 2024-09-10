using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Aplicacao.Dtos.Categoria;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Dominio.Enumeradores;
using CIFinance.Dominio.Extensoes;

namespace CIFinance.Aplicacao.Servicos;

public class CategoriaServico(IRepositorioEntidade<Categoria> repositorioCategoria, IRepositorioEntidade<Usuario> repositorioUsuario) : IServicoCrud<CategoriaDTO>
{
    readonly IRepositorioEntidade<Categoria> _repositorioCategoria = repositorioCategoria;
    readonly IRepositorioEntidade<Usuario> _repositorioUsuario = repositorioUsuario;

    public async Task<ServicoResposta<bool>> Atualizar(CategoriaDTO dto, string uidUsuario)
    {
        var resposta = new ServicoResposta<bool>(true);
        if (dto?.IdentificadorExterno is null)
        {
            resposta.ComErro("Idetificador de usuario invalido");
            return resposta;
        }

        try
        {
            if (await _repositorioCategoria.ObterAsync(dto.IdentificadorExterno) is Categoria categoria)
            {
                var temp = new Categoria(dto.Nome, dto.Tipo.ParaEnum<Tipo>(), categoria.Usuario);
                categoria.Atualizar(temp);
                await _repositorioCategoria.AtualizarAsync(categoria);

                resposta.ComSucesso(true);
            }
            else
            {
                resposta.ComErro("Categoria nao encontrada");
            }
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }

        return resposta;
    }

    public async Task<ServicoResposta<bool>> Criar(CategoriaDTO dto, string uidUsuario)
    {
        var resposta = new ServicoResposta<bool>(true);

        try
        {
            if (await _repositorioUsuario.ObterAsync(uidUsuario) is Usuario usuario)
            {
                var categoria = new Categoria(dto.Nome, dto.Tipo.ParaEnum<Tipo>(), usuario);
                await _repositorioCategoria.CriarAsync(categoria);

                resposta.ComSucesso(true);
            }
            else
            {
                resposta.ComErro("Nao e possivel criar uma categoria sem usuario.");
            }
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }
        return resposta;
    }

    public async Task<ServicoResposta<CategoriaDTO?>> Obter(string identificadorExterno, string uidUsuario)
    {
        var resposta = new ServicoResposta<CategoriaDTO?>(null);
        try
        {
            var resultado = await _repositorioCategoria.ObterAsync(identificadorExterno);
            if (resultado is Categoria categoria)
            {
                if (categoria.Usuario.IdentificadorExterno == uidUsuario)
                {
                    var mapeado = new CategoriaDTO
                    {
                        IdentificadorExterno = categoria.IdentificadorExterno,
                        Nome = categoria.Nome,
                        Tipo = categoria.Tipo
                    };
                    resposta.ComSucesso(mapeado);
                }
                else
                {
                    resposta.ComErro("Categoria nao encontrada.");
                }
            }
        }
        catch (ArgumentException e)
        {
            resposta.ComErro(e.Message);
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }

        return resposta;
    }

    public async Task<ServicoResposta<IEnumerable<CategoriaDTO?>>> ObterTodos(string uidUsuario)
    {
        var resposta = new ServicoResposta<IEnumerable<CategoriaDTO?>>([]);
        try
        {
            if ((await _repositorioCategoria.ObterTodosAsync())?.Where(c => c.Usuario.IdentificadorExterno == uidUsuario) is IEnumerable<Categoria> categorias)
            {
                resposta.ComSucesso(categorias.ToList().ConvertAll(c => new CategoriaDTO
                {
                    IdentificadorExterno = c.IdentificadorExterno,
                    Nome = c.Nome,
                    Tipo = c.Tipo
                }));
            }
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }

        return resposta;
    }

    public Task<ServicoResposta<bool>> Remover(string uidExterno, string uidUsuario)
    {
        throw new NotImplementedException();
    }
}
