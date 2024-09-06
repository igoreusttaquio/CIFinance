using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Dominio.Enumeradores;

namespace CIFinance.Aplicacao.Contratos.Categoria;

public record NovaCategoria(string Nome, Tipo Tipo, Usuario Usuario) : IDto;
