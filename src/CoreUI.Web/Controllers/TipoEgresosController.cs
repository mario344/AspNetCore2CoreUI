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
    public class TipoEgresosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoEgresosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoEgresos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoEgresos.ToListAsync());
        }

        // GET: TipoEgresos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEgresos = await _context.TipoEgresos
                .FirstOrDefaultAsync(m => m.TipoEgresosId == id);
            if (tipoEgresos == null)
            {
                return NotFound();
            }

            return View(tipoEgresos);
        }

        // GET: TipoEgresos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEgresos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucursalesId,TipoEgresosId,Nombre,Estatus,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] TipoEgresos tipoEgresos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEgresos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEgresos);
        }

        // GET: TipoEgresos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEgresos = await _context.TipoEgresos.FindAsync(id);
            if (tipoEgresos == null)
            {
                return NotFound();
            }
            return View(tipoEgresos);
        }

        // POST: TipoEgresos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SucursalesId,TipoEgresosId,Nombre,Estatus,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] TipoEgresos tipoEgresos)
        {
            if (id != tipoEgresos.TipoEgresosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEgresos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEgresosExists(tipoEgresos.TipoEgresosId))
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
            return View(tipoEgresos);
        }

        // GET: TipoEgresos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEgresos = await _context.TipoEgresos
                .FirstOrDefaultAsync(m => m.TipoEgresosId == id);
            if (tipoEgresos == null)
            {
                return NotFound();
            }

            return View(tipoEgresos);
        }

        // POST: TipoEgresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoEgresos = await _context.TipoEgresos.FindAsync(id);
            _context.TipoEgresos.Remove(tipoEgresos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEgresosExists(int id)
        {
            return _context.TipoEgresos.Any(e => e.TipoEgresosId == id);
        }
    }
}
