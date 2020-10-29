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
    public class PisosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PisosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pisos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pisos.ToListAsync());
        }

        // GET: Pisos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pisos = await _context.Pisos
                .FirstOrDefaultAsync(m => m.PisoId == id);
            if (pisos == null)
            {
                return NotFound();
            }

            return View(pisos);
        }

        // GET: Pisos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PisoId,Descripcion,Estatus")] Pisos pisos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pisos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pisos);
        }

        // GET: Pisos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pisos = await _context.Pisos.FindAsync(id);
            if (pisos == null)
            {
                return NotFound();
            }
            return View(pisos);
        }

        // POST: Pisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PisoId,Descripcion,Estatus")] Pisos pisos)
        {
            if (id != pisos.PisoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pisos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PisosExists(pisos.PisoId))
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
            return View(pisos);
        }

        // GET: Pisos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pisos = await _context.Pisos
                .FirstOrDefaultAsync(m => m.PisoId == id);
            if (pisos == null)
            {
                return NotFound();
            }

            return View(pisos);
        }

        // POST: Pisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pisos = await _context.Pisos.FindAsync(id);
            _context.Pisos.Remove(pisos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PisosExists(int id)
        {
            return _context.Pisos.Any(e => e.PisoId == id);
        }
    }
}
