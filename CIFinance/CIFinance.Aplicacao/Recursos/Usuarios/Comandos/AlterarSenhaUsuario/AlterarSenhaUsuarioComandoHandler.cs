﻿using CIFinance.Aplicacao.Abstracoes;
using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using MediatR;

namespace CIFinance.Aplicacao.Recursos.Usuarios.Comandos.AlterarSenhaUsuario;

public class AlterarSenhaUsuarioComandoHandler(IRepositorioUsuario repositorioUsuario,
    IServicoSenha senvicoSenha, IUnidadeTrabalho unidadeTrabalho) : IRequestHandler<AlterarSenhaUsuarioComando, Resultado<bool>>
{
    private readonly IRepositorioUsuario _repositorioUsuario = repositorioUsuario;
    private readonly IServicoSenha _senvicoSenha = senvicoSenha;
    private readonly IUnidadeTrabalho _unidadeTrabalho = unidadeTrabalho;
    public async Task<Resultado<bool>> Handle(AlterarSenhaUsuarioComando request, CancellationToken cancellationToken)
    {
        if (await _repositorioUsuario.ObterPorIdAsync(request.IdentificadorExterno) is Usuario usuario)
        {
            var saltoSenha = usuario.SaltoSenha;
            var hashSenhaAntiga = _senvicoSenha.ComputarHash(saltoSenha, request.SenhaAntiga);
            var hashSenhaNova = _senvicoSenha.ComputarHash(saltoSenha, request.SenhaNova);

            if (hashSenhaAntiga.Equals(hashSenhaNova) is false)
            {
                return UsuarioErros.SenhasNaoCombinam;
            }

            var novoHash = _senvicoSenha.GerarHashSenha(request.SenhaNova, out byte[] novoSalto);

            usuario.Atualizar(
                Usuario.Fabrica.Criar(usuario.Nome, usuario.Email, novoHash, novoSalto));

            _repositorioUsuario.Atualizar(usuario);
            await _unidadeTrabalho.SalvarAsync();
            return true;
        }

        return UsuarioErros.UsuarioNaoEncontrado;
    }
}
