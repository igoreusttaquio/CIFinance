using CIFinance.Dominio.Abstracoes;
using CIFinance.Dominio.Entidades;
using CIFinance.Dominio.Enumeradores;

namespace CIFinance.Aplicacao.Contratos.Categoria;

public record AtualizarCategoria(string Nome, Tipo Tipo, Usuario Usuario) : IDto;
