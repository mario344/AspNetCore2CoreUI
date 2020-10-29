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
    public class EgresosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EgresosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Egresos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Egresos.ToListAsync());
        }

        // GET: Egresos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egresos = await _context.Egresos
                .FirstOrDefaultAsync(m => m.EgresosId == id);
            if (egresos == null)
            {
                return NotFound();
            }

            return View(egresos);
        }

        // GET: Egresos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Egresos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucursalesId,EgresosId,TipoEgresosId,Descripcion,Cantidad,Ajuste,Cancelado,MotivoAjusteCancelacion,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Egresos egresos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(egresos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(egresos);
        }

        // GET: Egresos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egresos = await _context.Egresos.FindAsync(id);
            if (egresos == null)
            {
                return NotFound();
            }
            return View(egresos);
        }

        // POST: Egresos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SucursalesId,EgresosId,TipoEgresosId,Descripcion,Cantidad,Ajuste,Cancelado,MotivoAjusteCancelacion,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Egresos egresos)
        {
            if (id != egresos.EgresosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(egresos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EgresosExists(egresos.EgresosId))
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
            return View(egresos);
        }

        // GET: Egresos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egresos = await _context.Egresos
                .FirstOrDefaultAsync(m => m.EgresosId == id);
            if (egresos == null)
            {
                return NotFound();
            }

            return View(egresos);
        }

        // POST: Egresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var egresos = await _context.Egresos.FindAsync(id);
            _context.Egresos.Remove(egresos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EgresosExists(int id)
        {
            return _context.Egresos.Any(e => e.EgresosId == id);
        }
    }
}
