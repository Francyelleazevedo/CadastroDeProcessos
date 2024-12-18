using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroDeProcessos.Web.Models;
using CadastroDeProcessos.Application.Interfaces;
using CadastroDeProcessos.Domain.Entities;

namespace CadastroDeProcessos.Web.Controllers
{
    public class ProcessosController(IProcessoService processoService) : Controller
    {
        private readonly IProcessoService _processoService = processoService;

        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                int pageSize = 10;
                int pageNumber = page ?? 1;
                var pagedResult = await _processoService.GetAllAsync(pageNumber, pageSize);
                return View(pagedResult);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Ocorreu um erro ao carregar os processos.", Exception = ex });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var processo = await _processoService.GetByIdAsync(id);
                if (processo == null) return NotFound();
                return View(processo);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Ocorreu um erro ao carregar os detalhes do processo.", Exception = ex });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Processo processo)
        {
            if (!ModelState.IsValid) return View(processo);

            try
            {
                await _processoService.AddAsync(processo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Ocorreu um erro ao criar o processo.", Exception = ex });
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var processo = await _processoService.GetByIdAsync(id);
                if (processo == null) return NotFound();
                return View(processo);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Ocorreu um erro ao carregar o processo para edição.", Exception = ex });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Processo processo)
        {
            if (id != processo.Id) return NotFound();
            if (!ModelState.IsValid) return View(processo);

            try
            {
                await _processoService.UpdateAsync(processo);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProcessoExists(processo.Id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Ocorreu um erro ao atualizar o processo.", Exception = ex });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var processo = await _processoService.GetByIdAsync(id);
                if (processo == null) return NotFound();
                await _processoService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Ocorreu um erro ao excluir o processo.", Exception = ex });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarVisualizacao(int id)
        {
            try
            {
                await _processoService.ConfirmarVisualizacaoAsync(id);
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Ocorreu um erro ao confirmar a visualização do processo.", Exception = ex });
            }
        }

        private async Task<bool> ProcessoExists(int id)
        {
            var processo = await _processoService.GetByIdAsync(id);
            return processo != null;
        }
    }
}
