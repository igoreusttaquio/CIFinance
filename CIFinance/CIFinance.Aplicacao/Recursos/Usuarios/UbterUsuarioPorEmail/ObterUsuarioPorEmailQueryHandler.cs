using CIFinance.Aplicacao.Dtos.Usuario;
using CIFinance.Dominio.Entidades;
using CIFinance.Infra.Dados;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CIFinance.Aplicacao.Recursos.Usuarios.UbterUsuarioPorEmail;

public class ObterUsuarioPorEmailQueryHandler(BDContexto bDContexto) : IRequestHandler<ObterUsuarioPorEmailQuery, UsuarioDTO?>
{
    private readonly BDContexto _bdContexto = bDContexto;
    public async Task<UsuarioDTO?> Handle(ObterUsuarioPorEmailQuery request, CancellationToken cancellationToken)
    {
        if (await _bdContexto.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email) is Usuario usuario)
        {
            return new UsuarioDTO
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                IdentificadorExterno = usuario.IdentificadorExterno,
            };
        }
        return null;
    }
}
