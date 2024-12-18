using CadastroDeProcessos.Domain.Entities;

namespace CadastroDeProcessos.Domain.Interfaces;

public interface IProcessoRepository
{
    Task<int> GetTotalCountAsync();
    Task<List<Processo>> GetPagedAsync(int skip, int take);
    Task<Processo> GetByIdAsync(int id);
    Task AddAsync(Processo processo);
    Task UpdateAsync(Processo processo);
    Task DeleteAsync(int id);
}

