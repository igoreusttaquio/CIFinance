using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Aplicacao.Dtos.Conta;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;


namespace CIFinance.Aplicacao.Servicos;

public class ContaServico(IRepositorioEntidade<Conta> contaRepositorio, IRepositorioEntidade<Usuario> usuarioRepositorio) : IServicoCrud<ContaDTO>

{
    private readonly IRepositorioEntidade<Conta> _contaRepositorio = contaRepositorio;
    private readonly IRepositorioEntidade<Usuario> _usuarioRepositorio = usuarioRepositorio;
    public async Task<ServicoResposta<bool>> Atualizar(ContaDTO dto, string uidUsuario)
    {
        var resposta = new ServicoResposta<bool>(false);

        if (dto?.IdentificadorExterno is null)
        {
            resposta.ComErro("Identidicador de Conta inválido.");
            return resposta;
        }

        try
        {
            if (await _contaRepositorio.ObterAsync(dto.IdentificadorExterno) is not Conta conta)
            {
                resposta.ComErro("Conta não encontrada.");
                return resposta;
            }

            if (await _usuarioRepositorio.ObterAsync(uidUsuario) is not Usuario donoDaConta)
            {
                resposta.ComErro("Usuário não encontrado.");
                return resposta;
            }

            var ContaTemporaria = new Conta(dto.Nome, dto.Saldo, donoDaConta);
            conta.Atualizar(ContaTemporaria);
            await _contaRepositorio.AtualizarAsync(conta);
            resposta.ComSucesso(true);
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }
        return resposta;
    }

    public async Task<ServicoResposta<bool>> Criar(ContaDTO dto, string uidUsuario)
    {
        var resposta = new ServicoResposta<bool>(false);

        try
        {
            if (await _usuarioRepositorio.ObterAsync(uidUsuario) is Usuario donoDaConta)
            {
                if (donoDaConta is null)
                {
                    resposta.ComErro("Usuário não encontrado.");
                    return resposta;
                }
                var conta = new Conta(dto.Nome, dto.Saldo, donoDaConta);
                await _contaRepositorio.CriarAsync(conta);
                resposta.ComSucesso(true);
            }
            else
            {
                resposta.ComErro("Usuário não encontrado.");
            }
        }
        catch (Exception e)
        {
            resposta.ComErro(e.Message);
        }
        return resposta;
    }

    public Task<ServicoResposta<ContaDTO?>> Obter(string uidExterno, string uidUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<ServicoResposta<IEnumerable<ContaDTO?>>> ObterTodos(string uidUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<ServicoResposta<bool>> Remover(string uidExterno, string uidUsuario)
    {
        throw new NotImplementedException();
    }
}
