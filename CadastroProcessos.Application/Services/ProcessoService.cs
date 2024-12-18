using CadastroDeProcessos.Application.Interfaces;
using CadastroDeProcessos.Application.Common;
using CadastroDeProcessos.Domain.Entities;
using CadastroDeProcessos.Domain.Interfaces;

namespace CadastroDeProcessos.Application.Services
{
    public class ProcessoService(IProcessoRepository processoRepository) : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository = processoRepository;

        public async Task AddAsync(Processo processo)
        {
            if (processo == null) throw new ArgumentNullException(nameof(processo), "O processo não pode ser nulo.");
            try
            {
                processo.DataCadastro = DateTime.Now;
                await _processoRepository.AddAsync(processo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar o processo.", ex);
            }
        }

        public async Task<PagedResult<Processo>> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) throw new ArgumentException("O número da página deve ser maior que zero.", nameof(pageNumber));
            if (pageSize <= 0) throw new ArgumentException("O tamanho da página deve ser maior que zero.", nameof(pageSize));
            try
            {
                int totalItems = await _processoRepository.GetTotalCountAsync();
                int skip = (pageNumber - 1) * pageSize;
                var processos = await _processoRepository.GetPagedAsync(skip, pageSize);

                return new PagedResult<Processo>
                {
                    Items = processos,
                    TotalItems = totalItems,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a lista de processos.", ex);
            }
        }

        public async Task<Processo> GetByIdAsync(int id)
        {
            ValidateId(id);
            try
            {
                var processo = await _processoRepository.GetByIdAsync(id);
                return processo ?? throw new Exception("Processo não encontrado.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter o processo.", ex);
            }
        }

        public async Task UpdateAsync(Processo processo)
        {
            if (processo == null) throw new ArgumentNullException(nameof(processo), "O processo não pode ser nulo.");
            try
            {
                await _processoRepository.UpdateAsync(processo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o processo.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            ValidateId(id);
            try
            {
                var processo = await _processoRepository.GetByIdAsync(id) ?? throw new Exception("Processo não encontrado.");
                await _processoRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir o processo.", ex);
            }
        }

        public async Task ConfirmarVisualizacaoAsync(int id)
        {
            ValidateId(id);
            try
            {
                var processo = await _processoRepository.GetByIdAsync(id) ?? throw new Exception("Processo não encontrado.");
                processo.DataVisualizacao = DateTime.Now;
                await _processoRepository.UpdateAsync(processo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao confirmar a visualização do processo.", ex);
            }
        }

        private static void ValidateId(int id)
        {
            if (id <= 0) throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));
        }
    }
}
