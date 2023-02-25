using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace web.Controllers
{
    //Agustin77$$ --> Contraseña para el ingreso 
    //[Authorize] 
    public class ContratosController : Controller
    {
        private static int idContratoCrear;
        private static int idContratoEditar;
        private readonly MolinoContext _context;

        public ContratosController(MolinoContext context)
        {
            _context = context;
        }

        // GET: Contratos
        public async Task<IActionResult> Index(ContratoViewModel vm = null)
        {
            var contratos = _context.Contrato.Include(c => c.Empleado).Include(c => c.Grano).Include(c => c.Proveedor);
            if (vm.Filtro != null) contratos = contratos.Where(x => x.Numero.ToString() == vm.Filtro || x.MetodoDePago.ToLower() == vm.Filtro.ToLower() || x.PrecioTonelada.ToString() == vm.Filtro.ToLower()).Include(c => c.Proveedor);

            var contratosViewModels = new ContratoViewModel();
            contratosViewModels.Contratos = contratos;

            return View(contratosViewModels);
        }

        // GET: Contratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Empleado)
                .Include(c => c.Grano)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // GET: Contratos/Create
        public IActionResult Create()
        {
            ContratoCrearViewModel vm = new ContratoCrearViewModel();
            vm.Indicadores = new List<Indicador>();
            // Agrego a la lista de nombres los indicadores desde la base de datos
            vm.IndicadoresNombres = new SelectList(_context.IndicadorCalidad, "Nombre", "Nombre");
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Apellido");
            ViewData["GranoId"] = new SelectList(_context.Grano, "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre");
            return View(vm);
        }

        // POST: Contratos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContratoCrearViewModel vm)
        {
            if (true)
            {
                _context.Add(vm.Contrato);
                await _context.SaveChangesAsync();

                //System.Console.WriteLine(vm.Contrato.Id);
                idContratoCrear = vm.Contrato.Id;

                return RedirectToAction(nameof(Index));
            }

            // Limpiamos los campos para que vuelva a cargar la vista. También, podríamos redireccionar al listado de contratos
            vm = new ContratoCrearViewModel();
            vm.Contrato = new Contrato();
            vm.Indicadores = new List<Indicador>();
            // Agrego a la lista de nombres los indicadores desde la base de datos
            vm.IndicadoresNombres = new SelectList(_context.IndicadorCalidad, "Nombre", "Nombre");
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Apellido", vm.Contrato.EmpleadoId);
            ViewData["GranoId"] = new SelectList(_context.Grano, "Id", "Nombre", vm.Contrato.GranoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Cuit", vm.Contrato.ProveedorId);
            return View(vm);
        }

        // GET: Contratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ContratoCrearViewModel vm = new ContratoCrearViewModel();
            vm.Contrato = _context.Contrato.Find(id);
            vm.Indicadores = new List<Indicador>();
            vm.IndicadoresNombres = new SelectList(_context.IndicadorCalidad, "Id", "Nombre");
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Apellido");
            ViewData["GranoId"] = new SelectList(_context.Grano, "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre");
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }
            var listaIndicadores = _context.ValorIndicadorContrato.ToList().FindAll(i => i.ContratoId == id);
            foreach (var indicador in listaIndicadores)
            {
                var indi = new Indicador();
                indi.Valor = (int)indicador.Valor;
                foreach (var i in vm.IndicadoresNombres)
                {
                    if (indicador.IndicadorCalidadId.ToString() == i.Value)
                    {
                        indi.Nombre = i.Text;
                    }
                }
                vm.Indicadores.Add(indi);
            }
            vm.IndicadoresNombres = new SelectList(_context.IndicadorCalidad, "Nombre", "Nombre");
            return View(vm);
        }
        // POST: Contratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContratoCrearViewModel vm)
        {
            var contrato = vm.Contrato;
            idContratoEditar = contrato.Id;
            var listaIndicadores = _context.ValorIndicadorContrato.ToList().FindAll(i => i.ContratoId == idContratoEditar);
            _context.ValorIndicadorContrato.RemoveRange(listaIndicadores);
            _context.SaveChanges();
            _context.Update(contrato);
            await _context.SaveChangesAsync();
            Thread.Sleep(3000);
            return RedirectToAction("Index", "Contratos");
        }

        //POST: Json:Edit
        //[HttpPost]
        //public JsonResult ModificarDatos([FromBody] IEnumerable<Indicador> indicadores)
        //{
        //    Thread.Sleep(3000);
        //    if(indicadores != null)
        //    {
        //        indicadores = new List<Indicador>();
        //    }
        //    var vmm = new ContratoCrearViewModel();
        //    vmm.IndicadoresNombres = new SelectList(_context.IndicadorCalidad, "Id", "Nombre");
        //    foreach (var indi in indicadores)
        //        {
        //            var IndicadorCargar = new ValorIndicadorContrato();
        //            IndicadorCargar.Valor = indi.Valor;
        //            IndicadorCargar.ContratoId = idContratoEditar;    
        //            foreach (var ids in vmm.IndicadoresNombres)
        //            {
        //                if(ids.Text.Trim() == indi.Nombre)
        //                {
        //                    IndicadorCargar.IndicadorCalidadId = Int32.Parse(ids.Value.Trim());
        //                }
        //            }
        //            _context.Add(IndicadorCargar);
        //            _context.SaveChanges();
        //        }
        //    return Json(indicadores);
        //}

        public JsonResult ModificarDatos([FromBody] IEnumerable<Indicador> PD)
        {
            Thread.Sleep(2000);
            if (PD == null)
            {
                PD = new List<Indicador>();
            }
            var vmm = new ContratoCrearViewModel();
            vmm.IndicadoresNombres = new SelectList(_context.IndicadorCalidad, "Id", "Nombre");

            foreach (var indicador in PD)
            {
                var vm = new ValorIndicadorContrato();
                int id;
                vm.Valor = indicador.Valor;
                foreach (var ids in vmm.IndicadoresNombres)
                {
                    if (ids.Text == indicador.Nombre)
                    {
                        id = Int32.Parse(ids.Value);
                        vm.IndicadorCalidadId = id;
                    }
                }
                vm.ContratoId = idContratoEditar;

                _context.Add(vm);
                _context.SaveChanges();

            }
            return Json(PD);
        }

        // GET: Contratos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Empleado)
                .Include(c => c.Grano)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contrato == null)
            {
                return Problem("Entity set 'MolinoContext.Contrato'  is null.");
            }
            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato != null)
            {
                _context.Contrato.Remove(contrato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratoExists(int id)
        {
            return _context.Contrato.Any(e => e.Id == id);
        }

        // POST: Json
        [HttpPost]
        public JsonResult GuardarDatos([FromBody] IEnumerable<Indicador> PD)
        {
            Thread.Sleep(2000);
            if (PD == null)
            {
                PD = new List<Indicador>();
            }
            var vmm = new ContratoCrearViewModel();
            vmm.IndicadoresNombres = new SelectList(_context.IndicadorCalidad, "Id", "Nombre");
            //Loop and insert records.
            foreach (var indicador in PD)
            {
                var vm = new ValorIndicadorContrato();
                int id;
                vm.Valor = indicador.Valor;
                foreach (var ids in vmm.IndicadoresNombres)
                {
                    if (ids.Text == indicador.Nombre)
                    {
                        id = Int32.Parse(ids.Value);
                        vm.IndicadorCalidadId = id;
                    }
                }
                vm.ContratoId = idContratoCrear;

                _context.Add(vm);
                _context.SaveChanges();

            }
            return Json(PD);
        }
    }

}