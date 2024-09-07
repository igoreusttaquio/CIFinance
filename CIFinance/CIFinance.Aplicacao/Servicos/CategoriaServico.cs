using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Aplicacao.Dtos.Categoria;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Dominio.Enumeradores;
using CIFinance.Dominio.Extensoes;

namespace CIFinance.Aplicacao.Servicos;

public class CategoriaServico(IRepositorioEntidade<Categoria> repositorio) : IServico<CategoriaDTO>
{
    IRepositorioEntidade<Categoria> _repositorio = repositorio;

    public async Task Atualizar(CategoriaDTO dto, string uidUsuario)
    {
        if (dto?.IdentificadorExterno is null)
        {
            return;
        }

        if (await _repositorio.Obter(dto.IdentificadorExterno) is Categoria categoria)
        {
            var temp = new Categoria(dto.Nome, dto.Tipo.ParaEnum<Tipo>(), categoria.Usuario);
            categoria.Atualizar(temp);
            await _repositorio.Atualizar(categoria);
        }
    }

    public async Task Criar(CategoriaDTO dto, string uidUsuario)
    {
        var u = new Usuario("Igor", "igoreusttaquio@gmail.com");
        var categoria = new Categoria(dto.Nome, dto.Tipo.ParaEnum<Tipo>(), u);
        await _repositorio.Criar(categoria);
    }

    public async Task<ServicoResposta<CategoriaDTO?>> Obter(string uidExterno, string uidUsuario)
    {
        var resposta = new ServicoResposta<CategoriaDTO?>();
        try
        {
            var resultado = await _repositorio.Obter(uidExterno);
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

    public Task Remover(CategoriaDTO dto)
    {
        throw new NotImplementedException();
    }
}
