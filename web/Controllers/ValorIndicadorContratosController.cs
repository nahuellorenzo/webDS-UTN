using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Controllers
{
    public class ValorIndicadorContratosController : Controller
    {
        private readonly MolinoContext _context;

        public ValorIndicadorContratosController(MolinoContext context)
        {
            _context = context;
        }

        // GET: ValorIndicadorContratos
        public async Task<IActionResult> Index()
        {
            var molinoContext = _context.ValorIndicadorContrato.Include(v => v.Contrato).Include(v => v.IndicadorCalidad);
            return View(await molinoContext.ToListAsync());
        }

        // GET: ValorIndicadorContratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ValorIndicadorContrato == null)
            {
                return NotFound();
            }

            var valorIndicadorContrato = await _context.ValorIndicadorContrato
                .Include(v => v.Contrato)
                .Include(v => v.IndicadorCalidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valorIndicadorContrato == null)
            {
                return NotFound();
            }

            return View(valorIndicadorContrato);
        }

        // GET: ValorIndicadorContratos/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contrato, "Id", "MetodoDePago");
            ViewData["IndicadorCalidadId"] = new SelectList(_context.IndicadorCalidad, "Id", "Nombre");
            return View();
        }

        // POST: ValorIndicadorContratos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valor,ContratoId,IndicadorCalidadId")] ValorIndicadorContrato valorIndicadorContrato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valorIndicadorContrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contrato, "Id", "MetodoDePago", valorIndicadorContrato.ContratoId);
            ViewData["IndicadorCalidadId"] = new SelectList(_context.IndicadorCalidad, "Id", "Nombre", valorIndicadorContrato.IndicadorCalidadId);
            return View(valorIndicadorContrato);
        }

        // GET: ValorIndicadorContratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ValorIndicadorContrato == null)
            {
                return NotFound();
            }

            var valorIndicadorContrato = await _context.ValorIndicadorContrato.FindAsync(id);
            if (valorIndicadorContrato == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contrato, "Id", "MetodoDePago", valorIndicadorContrato.ContratoId);
            ViewData["IndicadorCalidadId"] = new SelectList(_context.IndicadorCalidad, "Id", "Nombre", valorIndicadorContrato.IndicadorCalidadId);
            return View(valorIndicadorContrato);
        }

        // POST: ValorIndicadorContratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor,ContratoId,IndicadorCalidadId")] ValorIndicadorContrato valorIndicadorContrato)
        {
            if (id != valorIndicadorContrato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valorIndicadorContrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValorIndicadorContratoExists(valorIndicadorContrato.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contrato, "Id", "MetodoDePago", valorIndicadorContrato.ContratoId);
            ViewData["IndicadorCalidadId"] = new SelectList(_context.IndicadorCalidad, "Id", "Nombre", valorIndicadorContrato.IndicadorCalidadId);
            return View(valorIndicadorContrato);
        }

        // GET: ValorIndicadorContratos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ValorIndicadorContrato == null)
            {
                return NotFound();
            }

            var valorIndicadorContrato = await _context.ValorIndicadorContrato
                .Include(v => v.Contrato)
                .Include(v => v.IndicadorCalidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valorIndicadorContrato == null)
            {
                return NotFound();
            }

            return View(valorIndicadorContrato);
        }

        // POST: ValorIndicadorContratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ValorIndicadorContrato == null)
            {
                return Problem("Entity set 'MolinoContext.ValorIndicadorContrato'  is null.");
            }
            var valorIndicadorContrato = await _context.ValorIndicadorContrato.FindAsync(id);
            if (valorIndicadorContrato != null)
            {
                _context.ValorIndicadorContrato.Remove(valorIndicadorContrato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValorIndicadorContratoExists(int id)
        {
          return _context.ValorIndicadorContrato.Any(e => e.Id == id);
        }
    }
}
