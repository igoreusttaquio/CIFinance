using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Aplicacao.Dtos.Transacao;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Dominio.Enumeradores;
using CIFinance.Dominio.Extensoes;

namespace CIFinance.Aplicacao.Servicos;

public class TransacaoServico(IRepositorioEntidade<Transacao> transacaoRepositorio, IRepositorioEntidade<Categoria> categoriaRepositorio,
    IRepositorioEntidade<Conta> contaRepositorio) : IServicoCrud<TransacaoDTO>
{
    public readonly IRepositorioEntidade<Transacao> _transacaoRepositorio = transacaoRepositorio;
    public readonly IRepositorioEntidade<Categoria> _categoiriaRepositorio = categoriaRepositorio;
    public readonly IRepositorioEntidade<Conta> _contaRepositorio = contaRepositorio;

    public async Task<ServicoResposta<bool>> Remover(string uidExterno, string uidUsuario)
    {
        var resposta = new ServicoResposta<bool>(false);

        try
        {
            if (await _transacaoRepositorio.ObterAsync(uidExterno) is Transacao transacao)
            {
                if (transacao.Conta.Usuario.IdentificadorExterno == uidUsuario)
                {
                    await _transacaoRepositorio.ExcluirAsync(transacao);
                    resposta.ComSucesso(true);
                }
                else
                    resposta.ComErro("Usuario nao e dono da transacao");
            }
            else
                resposta.ComErro("Transacao nao encontrada para remocao");
        }
        catch (Exception ex)
        {
            resposta.ComErro(ex.Message);
        }
        return resposta;
    }

    public async Task<ServicoResposta<bool>> Atualizar(TransacaoDTO dto, string uidUsuario)
    {
        var resposta = new ServicoResposta<bool>(false);
        try
        {
            if (dto?.IdentificadorExterno is null)
            {
                resposta.ComErro("Id invalido para a transacao");
                return resposta;
            }

            if (await _transacaoRepositorio.ObterAsync(dto.IdentificadorExterno) is Transacao transacao)
            {
                var conta = await _contaRepositorio.ObterAsync(dto.IdConta);
                var categoria = await _categoiriaRepositorio.ObterAsync(dto.IdCategoria);

                if (categoria is null || conta is null)
                {
                    resposta.ComErro("Nao e possivel criar uma transacao sem uma conta ou categoria");
                    return resposta;
                }

                var mapeado = new Transacao(conta, categoria, dto.Descricao, dto.Valor, dto.Tipo.ParaEnum<Tipo>(), dto.Data);
                transacao.Atualizar(mapeado);
                await _transacaoRepositorio.AtualizarAsync(transacao);
                resposta.ComSucesso(true);
            }
        }
        catch (Exception ex)
        {
            resposta.ComErro(ex.Message);
        }

        return resposta;
    }

    public Task<ServicoResposta<bool>> Criar(TransacaoDTO dto, string uidUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<ServicoResposta<TransacaoDTO?>> Obter(string uidExterno, string uidUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<ServicoResposta<IEnumerable<TransacaoDTO?>>> ObterTodos(string uidUsuario)
    {
        throw new NotImplementedException();
    }
}
