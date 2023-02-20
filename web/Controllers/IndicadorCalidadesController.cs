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
    public class IndicadorCalidadesController : Controller
    {
        private readonly MolinoContext _context;

        public IndicadorCalidadesController(MolinoContext context)
        {
            _context = context;
        }

        // GET: IndicadorCalidades
        public async Task<IActionResult> Index()
        {
              return View(await _context.IndicadorCalidad.ToListAsync());
        }

        // GET: IndicadorCalidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IndicadorCalidad == null)
            {
                return NotFound();
            }

            var indicadorCalidad = await _context.IndicadorCalidad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indicadorCalidad == null)
            {
                return NotFound();
            }

            return View(indicadorCalidad);
        }

        // GET: IndicadorCalidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IndicadorCalidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] IndicadorCalidadesContrato indicadorCalidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(indicadorCalidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(indicadorCalidad);
        }

        // GET: IndicadorCalidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IndicadorCalidad == null)
            {
                return NotFound();
            }

            var indicadorCalidad = await _context.IndicadorCalidad.FindAsync(id);
            if (indicadorCalidad == null)
            {
                return NotFound();
            }
            return View(indicadorCalidad);
        }

        // POST: IndicadorCalidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] IndicadorCalidadesContrato indicadorCalidad)
        {
            if (id != indicadorCalidad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indicadorCalidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndicadorCalidadExists(indicadorCalidad.Id))
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
            return View(indicadorCalidad);
        }

        // GET: IndicadorCalidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IndicadorCalidad == null)
            {
                return NotFound();
            }

            var indicadorCalidad = await _context.IndicadorCalidad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indicadorCalidad == null)
            {
                return NotFound();
            }

            return View(indicadorCalidad);
        }

        // POST: IndicadorCalidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IndicadorCalidad == null)
            {
                return Problem("Entity set 'MolinoContext.IndicadorCalidad'  is null.");
            }
            var indicadorCalidad = await _context.IndicadorCalidad.FindAsync(id);
            if (indicadorCalidad != null)
            {
                _context.IndicadorCalidad.Remove(indicadorCalidad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndicadorCalidadExists(int id)
        {
          return _context.IndicadorCalidad.Any(e => e.Id == id);
        }
    }
}
