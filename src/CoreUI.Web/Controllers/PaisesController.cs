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
    public class PaisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Paises
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paises.ToListAsync());
        }

        public List<Object[]>ListarPaises(int pagina,string busqueda) 
        {
            string dataFilter = "", estatusPaises = "";
            List<Object[]> data = new List<object[]>();

            var query = (from p in _context.Paises.ToList()
                         select new
                         {
                             p.PaisId,
                             p.Nombre,
                             p.Estatus
                         }).OrderBy(c => c.Nombre);

            foreach (var item in query)
            {
                if(item.Estatus == true) 
                {
                    estatusPaises = "Activo";
                }
                else if(item.Estatus == false) 
                {
                    estatusPaises = "Inactivo";
                }

                dataFilter +=
                    "<tr>"+
                        "<th>" +
                        item.Nombre +
                        "</th>" +
                        "<th>" +
                        estatusPaises +
                        "</th>"+
                        "<th>"+
                         "<a class='btn btn-success active'>Editar</a>"+
                         "<a class='btn btn-info active'>Detalles</a>" +
                         "<a class='btn btn-danger active'>Editar</a>" +
                         "</th>"+
                    "</tr>";
            }
            Object[] dataObj = { dataFilter };
            data.Add(dataObj);

            return data;
        }


        // GET: Paises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paises = await _context.Paises
                .SingleOrDefaultAsync(m => m.PaisId == id);
            if (paises == null)
            {
                return NotFound();
            }

            return View(paises);
        }

        // GET: Paises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaisId,Nombre,Estatus")] Paises paises)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paises);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paises);
        }

        // GET: Paises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paises = await _context.Paises.SingleOrDefaultAsync(m => m.PaisId == id);
            if (paises == null)
            {
                return NotFound();
            }
            return View(paises);
        }

        // POST: Paises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaisId,Nombre,Estatus")] Paises paises)
        {
            if (id != paises.PaisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paises);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisesExists(paises.PaisId))
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
            return View(paises);
        }

        // GET: Paises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paises = await _context.Paises
                .SingleOrDefaultAsync(m => m.PaisId == id);
            if (paises == null)
            {
                return NotFound();
            }

            return View(paises);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paises = await _context.Paises.SingleOrDefaultAsync(m => m.PaisId == id);
            _context.Paises.Remove(paises);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisesExists(int id)
        {
            return _context.Paises.Any(e => e.PaisId == id);
        }
    }
}
