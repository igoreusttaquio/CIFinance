using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Aplicacao.Dtos.Categoria;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Dominio.Enumeradores;
using CIFinance.Dominio.Extensoes;

namespace CIFinance.Aplicacao.Servicos;

public class CategoriaServico(IRepositorioEntidade<Categoria> repositorio) : IServicoCrud<CategoriaDTO>
{
    IRepositorioEntidade<Categoria> _repositorio = repositorio;

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
            if (await _repositorio.ObterAsync(dto.IdentificadorExterno) is Categoria categoria)
            {
                var temp = new Categoria(dto.Nome, dto.Tipo.ParaEnum<Tipo>(), categoria.Usuario);
                categoria.Atualizar(temp);
                await _repositorio.AtualizarAsync(categoria);

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
            var u = new Usuario("Igor", "igoreusttaquio@gmail.com");
            var categoria = new Categoria(dto.Nome, dto.Tipo.ParaEnum<Tipo>(), u);
            await _repositorio.CriarAsync(categoria);

            resposta.ComSucesso(true);
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }
        return resposta;
    }

    public async Task<ServicoResposta<CategoriaDTO?>> Obter(string uidExterno, string uidUsuario)
    {
        var resposta = new ServicoResposta<CategoriaDTO?>(null);
        try
        {
            var resultado = await _repositorio.ObterAsync(uidExterno);
            if (resultado is Categoria categoria)
            {
                var mapeado = new CategoriaDTO
                {
                    IdentificadorExterno = categoria.IdentificadorExterno,
                    Nome = categoria.Nome,
                    Tipo = categoria.Tipo
                };

                resposta.ComSucesso(mapeado);
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

    public Task<ServicoResposta<IEnumerable<CategoriaDTO>?>> ObterTodos(string uidUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<ServicoResposta<bool>> Remover(CategoriaDTO dto)
    {
        throw new NotImplementedException();
    }
}
