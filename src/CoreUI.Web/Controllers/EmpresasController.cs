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
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Empresas.Include(e => e.Estados).Include(e => e.Paises);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas
                .Include(e => e.Estados)
                .Include(e => e.Paises)
                .SingleOrDefaultAsync(m => m.EmpresaId == id);
            if (empresas == null)
            {
                return NotFound();
            }

            return View(empresas);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "EstadoId");
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpresaId,Nombre,Direccion,Telefono,Estatus,PaisId,EstadoId,Id,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Empresas empresas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "EstadoId", empresas.EstadoId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", empresas.PaisId);
            return View(empresas);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas.SingleOrDefaultAsync(m => m.EmpresaId == id);
            if (empresas == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "EstadoId", empresas.EstadoId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", empresas.PaisId);
            return View(empresas);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpresaId,Nombre,Direccion,Telefono,Estatus,PaisId,EstadoId,Id,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Empresas empresas)
        {
            if (id != empresas.EmpresaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresasExists(empresas.EmpresaId))
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "EstadoId", empresas.EstadoId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", empresas.PaisId);
            return View(empresas);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas
                .Include(e => e.Estados)
                .Include(e => e.Paises)
                .SingleOrDefaultAsync(m => m.EmpresaId == id);
            if (empresas == null)
            {
                return NotFound();
            }

            return View(empresas);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresas = await _context.Empresas.SingleOrDefaultAsync(m => m.EmpresaId == id);
            _context.Empresas.Remove(empresas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresasExists(int id)
        {
            return _context.Empresas.Any(e => e.EmpresaId == id);
        }
    }
}
