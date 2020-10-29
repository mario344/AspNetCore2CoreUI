using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreUI.Web.Data;
using CoreUI.Web.Models;

namespace CoreUI.Web.Controllers
{
    public class IngresosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngresosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingresos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingresos.ToListAsync());
        }

        // GET: Ingresos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresos = await _context.Ingresos
                .FirstOrDefaultAsync(m => m.IngresosId == id);
            if (ingresos == null)
            {
                return NotFound();
            }

            return View(ingresos);
        }

        // GET: Ingresos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingresos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucursalesId,IngresosId,TiposIngresosId,Descripcion,Cantidad,Ajuste,Cancelado,MotivoAjusteCancelacion,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingresos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingresos);
        }

        // GET: Ingresos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresos = await _context.Ingresos.FindAsync(id);
            if (ingresos == null)
            {
                return NotFound();
            }
            return View(ingresos);
        }

        // POST: Ingresos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SucursalesId,IngresosId,TiposIngresosId,Descripcion,Cantidad,Ajuste,Cancelado,MotivoAjusteCancelacion,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Ingresos ingresos)
        {
            if (id != ingresos.IngresosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingresos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresosExists(ingresos.IngresosId))
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
            return View(ingresos);
        }

        // GET: Ingresos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresos = await _context.Ingresos
                .FirstOrDefaultAsync(m => m.IngresosId == id);
            if (ingresos == null)
            {
                return NotFound();
            }

            return View(ingresos);
        }

        // POST: Ingresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingresos = await _context.Ingresos.FindAsync(id);
            _context.Ingresos.Remove(ingresos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngresosExists(int id)
        {
            return _context.Ingresos.Any(e => e.IngresosId == id);
        }
    }
}
