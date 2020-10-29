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
    public class EstadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Estados
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Estados.Include(e => e.Paises);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Estados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados
                .Include(e => e.Paises)
                .SingleOrDefaultAsync(m => m.EstadoId == id);
            if (estados == null)
            {
                return NotFound();
            }

            return View(estados);
        }

        // GET: Estados/Create
        public IActionResult Create()
        {
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId");
            return View();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoId,Nombre,Estatus,PaisId")] Estados estados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estados.PaisId);
            return View(estados);
        }

        // GET: Estados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados.SingleOrDefaultAsync(m => m.EstadoId == id);
            if (estados == null)
            {
                return NotFound();
            }
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estados.PaisId);
            return View(estados);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoId,Nombre,Estatus,PaisId")] Estados estados)
        {
            if (id != estados.EstadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosExists(estados.EstadoId))
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
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estados.PaisId);
            return View(estados);
        }

        // GET: Estados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados
                .Include(e => e.Paises)
                .SingleOrDefaultAsync(m => m.EstadoId == id);
            if (estados == null)
            {
                return NotFound();
            }

            return View(estados);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estados = await _context.Estados.SingleOrDefaultAsync(m => m.EstadoId == id);
            _context.Estados.Remove(estados);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosExists(int id)
        {
            return _context.Estados.Any(e => e.EstadoId == id);
        }
    }
}
