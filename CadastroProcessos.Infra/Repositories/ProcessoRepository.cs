using Microsoft.EntityFrameworkCore;
using CadastroDeProcessos.Domain.Entities;
using CadastroDeProcessos.Domain.Interfaces;
using CadastroDeProcessos.Infra.Context;
using CadastroDeProcessos.Infra.Exceptions;

namespace CadastroDeProcessos.Infra.Repositories
{
    public class ProcessoRepository(ApplicationDbContext context) : IProcessoRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Processo> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));

            try
            {
                return await _context.Processos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id)
                    ?? throw new Exception("Processo não encontrado.");
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException("Erro ao buscar o processo no banco de dados.", ex);
            }
        }

        public async Task AddAsync(Processo processo)
        {
            if (processo == null)
                throw new ArgumentNullException(nameof(processo), "O processo não pode ser nulo.");

            try
            {
                await _context.Processos.AddAsync(processo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException("Erro ao adicionar o processo no banco de dados.", ex);
            }
        }

        public async Task UpdateAsync(Processo processo)
        {
            if (processo == null)
                throw new ArgumentNullException(nameof(processo), "O processo não pode ser nulo.");

            try
            {
                _context.Processos.Update(processo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InfrastructureException("Erro de concorrência ao atualizar o processo.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException("Erro ao atualizar o processo no banco de dados.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));

            try
            {
                var processo = await GetByIdAsync(id) ?? throw new Exception("Processo não encontrado.");
                _context.Processos.Remove(processo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException("Erro ao excluir o processo no banco de dados.", ex);
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            try
            {
                return await _context.Processos.CountAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException("Erro ao contar o número total de processos.", ex);
            }
        }

        public async Task<List<Processo>> GetPagedAsync(int skip, int take)
        {
            if (skip < 0)
                throw new ArgumentException("O valor de 'skip' não pode ser negativo.", nameof(skip));
            if (take <= 0)
                throw new ArgumentException("O valor de 'take' deve ser maior que zero.", nameof(take));

            try
            {
                return await _context.Processos
                    .AsNoTracking()
                    .OrderByDescending(p => p.Id)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InfrastructureException("Erro ao obter a lista paginada de processos.", ex);
            }
        }
    }
}
