namespace CIFinance.Dominio.Abstracoes;

public interface IEntidade
{
    int Id { get; }
    DateTime Criacao { get; }
    DateTime UltimaAlteracao { get; }
    string IdentificadorExterno { get; }
    bool Ativo { get; }

    void Atualizar<TEntidade>(TEntidade entidade);
    void Inativar();
    void AtualizarDataUltimaAlteracao();
}
