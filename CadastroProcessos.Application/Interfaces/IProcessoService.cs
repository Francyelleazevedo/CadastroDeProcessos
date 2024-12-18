using CadastroDeProcessos.Application.Common;
using CadastroDeProcessos.Domain.Entities;

namespace CadastroDeProcessos.Application.Interfaces;

public interface IProcessoService
{
    Task AddAsync(Processo processo);
    Task<PagedResult<Processo>> GetAllAsync(int pageNumber, int pageSize);
    Task<Processo> GetByIdAsync(int id);
    Task UpdateAsync(Processo processo);
    Task DeleteAsync(int id);
    Task ConfirmarVisualizacaoAsync(int id);
}
