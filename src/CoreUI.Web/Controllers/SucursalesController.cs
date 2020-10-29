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
    public class SucursalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SucursalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sucursales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sucursales.Include(s => s.Empresas).Include(s => s.Paises);
            return View(await applicationDbContext.ToListAsync());
        }


        public List<Object[]>ListarSucursales(int pagina,string busqueda) 
        
        {


            // int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 15;
            // int can_paginas;
            // string dataFilter = "", paginador = "", Estado = null, tipo = null, boton = ""; 
            string dataFilter = "",estatusSucursal = "";
            List<object[]> data = new List<object[]>();

            var query = (from s in _context.Sucursales.ToList()
                         join e in _context.Empresas
                         on s.EmpresaId equals e.EmpresaId
                         join p in _context.Paises
                         on s.EstadoId equals p.PaisId
                         join es in _context.Estados
                         on s.EstadoId equals es.EstadoId
                         select new
                         {
                             s.SucursalesId,
                             s.Nombre,
                             s.Direccion,
                             s.Telefono,
                             s.Estatus,
                             e.EmpresaId,
                            
                             p.PaisId,
                            nombrep = p.Nombre,
                            nombree = e.Nombre

                         }).OrderBy(c => c.Nombre);

            if (query.Count() > 0)
            {


            
                foreach (var item in query)
            {
                //condicion para los estatus
                if (item.Estatus == true)
                {
                    estatusSucursal = "Activo";
                }
                else if(item.Estatus == false)
                {
                    estatusSucursal = "Inactivo";
                }

                dataFilter +=

                "<tr>" +
                 "<th>" +
                        item.nombree +
                    "</th>" +
                    "<th>" +
                        item.Nombre +
                    "</th>" +
                    "<th>" +
                       item.Direccion +
                    "</th>" +
                    "<th>" +
                        item.Telefono +
                    "</th>" +
                    "<th>" +
                       estatusSucursal +
                    "</th>" +
                        item.nombree +
                    "<th>" +
                       item.nombrep +
                    "</th>" +
                  "<th>" +
                       item.PaisId +
                    "</th>" +
                    "<th>" +
                     "<a class='btn btn-success active' data-toggle='modal' data-target='#modalSucursal' onclick='creaModalSucursales(2, " +item.SucursalesId + ")'> Editar</a> |" +
                     "<a class='btn btn-info active' data-toggle='modal' data-target='#modalSucursal' onclick='creaModalSucursales(3, " + item.SucursalesId + ")'> Detalles</a> |" +
                     "<a class='btn btn-danger active' data-toggle='modal' data-target='#modalSucursal' onclick='creaModalSucursales(4, " + item.SucursalesId + ")'> Editar</a> |" +
                    "</th>" +
            "</tr>";

            }
            }

            Object[] dataObj = { dataFilter };
            data.Add(dataObj);

            return data;
        }

        ///Metodo de modal Sucursales
        ///
        public List<Object[]> CreaModalSucursales(byte funcion, int id)
        {
            string dataFilter = "", ddlempresa = "", ddlEstado="",ddlPais="";
            List<object[]> data = new List<object[]>();

            if(funcion == 1) 
            {


                var empresaList = (from s in _context.Empresas.ToList()
                                   where s.Estatus == true
                                   select new
                                   {
                                       s.EmpresaId,
                                       s.Nombre
                                   }).OrderBy(c => c.Nombre);

                var estadoList = (from e in _context.Estados.ToList()
                                  where e.Estatus == true
                                  select new
                                  {
                                      e.EstadoId,
                                      e.Nombre
                                  }).OrderBy(c => c.Nombre);

                var paisList = (from p in _context.Paises.ToList()
                                where p.Estatus == true
                                select new
                                {
                                    p.PaisId,
                                    p.Nombre
                                }).OrderBy(c => c.Nombre);

                ///////////listas
                ///

                ddlempresa = "<option value = '0'> Seleccione una empresa </option>";
                foreach (var item in empresaList)
                {
                    ddlempresa += "<option value = '" + item.EmpresaId + "'>" + item.Nombre + " </option>";
                }

                ddlEstado = "<option value = '0'> Seleccione un Estado </option>";
                foreach (var item in estadoList)
                {
                    ddlEstado += "<option value ='" + item.EstadoId + "'>" + item.Nombre + " </option>";
                }

                ddlPais = "<option value = '0'> Seleccione un Pais </option>";
                foreach (var item in paisList)
                {
                    ddlPais += "<option value ='" + item.PaisId + "'>" + item.Nombre + " </option>";
                }

                dataFilter = "<div class='row'>" +
                                 "<div class='col-md-4'>" +
                                      "<form>" +
                             "<div class='form-group'>" +
                             "<label for='EmpresaId' class='control-label'>Empresa ID </label>" +
                             "<select for ='EmpresaId' id='EmpresaId' class='form-control'>" +
                             ddlempresa +
                             "</select>" +
                             "</div>" +
                             "<div class='form-group'>" +
                             "<label for ='Nombre' class='control-label'>Nombre</label>" +
                             "<input for ='Nombre' id='Nombre' class='form-control'/>" +
                             "</div>" +
                             "<div class='form-group'>" +
                              "<label for ='Estatus' class='control-label'>Estatus</label>" +
                             "<input for ='Estatus' id='Estatus' class='form-control'/>" +
                             "</div>" +
                             "<div class='form-group'>" +
                              "<label for ='Telefono' class='control-label'>Telefono</label>" +
                             "<input for ='Telefono' id='Estatus' class='form-control'/>" +
                             "</div>" +
                             "<div class='form-group'>" +
                              "<label for ='direccion' class='control-label'>Direccion</label>" +
                             "<input for ='direccion' id='direccion' class='form-control'/>" +
                             "</div>" +
                              "<div class='form-group'>" +
                             "<label for='EstadoId' class='control-label'>Estado</label>" +
                             "<select for ='EstadoId' id='EstadoId' class='form-control'>" +
                             ddlEstado +
                             "</select>" +
                             "</div>" +
                             "<div class='form-group'>" +
                             "<label for='EstadoId' class='control-label'> Pais</label>" +
                             "<select for ='EstadoId' id='EstadoId' class='form-control'>" +
                              ddlPais +
                             "</select>" +
                             "</div>" +
                             "<div class='form-group'>" +
                             "<button class='btn btn-success' onclick='guardaSucursales(1,0)'> Agregar </button>" +
                             "</div>" +
                             "</form>" +
                             "</div>" +
                             "</div>";
                               }


            else if (funcion == 2) //edita
            
            {
                var sucursal = from s in _context.Sucursales.ToList()
                               join e in _context.Empresas
                               on s.EmpresaId equals e.EmpresaId
                               join p in _context.Paises
                               on s.EstadoId equals p.PaisId
                               join es in _context.Estados
                               on s.EstadoId equals es.EstadoId
                               where s.SucursalesId == id

                               select new
                               {
                                   s.SucursalesId,
                                   s.Nombre,
                                   s.Direccion,
                                   s.Telefono,
                                   s.Estatus,
                                   e.EmpresaId,
                                   s.UsrAlta,
                                   s.UsrFechaAlta,
                                   s.UsrFechaMod,
                                   s.UsrMod,

                                   p.PaisId,
                                   nombrep = p.Nombre,
                                   nombree = e.Nombre,
                                   es.EstadoId
                               };


                var empresasList = (from e in _context.Sucursales.ToList()
                                    where e.Estatus == true
                                    select new
                                    {
                                        e.EmpresaId,
                                        e.Nombre
                                    }).OrderBy(c => c.Nombre);

                var paisesList = (from p in _context.Paises.ToList()
                                  where p.Estatus == true
                                  select new
                                  {
                                      p.PaisId,
                                      p.Nombre
                                  }).OrderBy(c => c.Nombre);

                var estadosList = (from es in _context.Estados.ToList()
                                   where es.Estatus == true
                                   select new
                                   {
                                       es.EstadoId,
                                       es.Nombre
                                   }).OrderBy(c => c.Nombre);

                //listas

                ddlempresa = "<option value ='" + sucursal.First().EmpresaId + "' selected> " + sucursal.First().Nombre + "</option>";
                foreach (var item in empresasList)
                {
                    ddlempresa += "<option value ='" + item.EmpresaId + "'>" + item.Nombre + "</option>";
                }

                ddlPais = "<option value ='" + sucursal.First().PaisId + "' selected> " + sucursal.First().nombrep + "</option>";
                foreach (var item in paisesList)
                {
                    ddlPais += "<option value ='" + item.PaisId + "'>" + item.Nombre + "</option>";
                }

                ddlEstado = "<option value ='" + sucursal.First().EstadoId + "' selected> " + sucursal.First().nombree + "</option>";
                foreach (var item in estadosList)
                {
                    ddlEstado += "<option value ='" + item.EstadoId + "'>" + item.Nombre + "</option>";
                }

                dataFilter =
                    "<div class='row'>" +
                                 "<div class='col-md-4'>" +
                                      "<form>" +
                             "<div class='form-group'>" +
                             "<label for='EmpresaId' class='control-label'>Empresa ID </label>" +
                             "<select for ='EmpresaId' id='EmpresaId' class='form-control'>" +
                             ddlempresa +
                             "</select>" +
                             "</div>" +
                             "<div class='form-group'>" +
                             "<label for ='Nombre' class='control-label'>Nombre</label>" +
                             "<input for ='Nombre' id='Nombre' value = '" + sucursal.First().Nombre + "' class='form-control'/>" +
                             "</div>" +
                             "<div class='form-group'>" +
                              "<label for ='Estatus' class='control-label'>Estatus</label>" +
                             "<input for ='Estatus' id='Estatus' value = '" + sucursal.First().Estatus + "' class='form-control'/>" +
                             "</div>" +
                             "<div class='form-group'>" +
                              "<label for ='Telefono' class='control-label'>Telefono</label>" +
                             "<input for ='Telefono' id='Estatus' value = '" + sucursal.First().Telefono + "' class='form-control'/>" +
                             "</div>" +
                             "<div class='form-group'>" +
                              "<label for ='direccion' class='control-label'>Direccion</label>" +
                             "<input for ='direccion' id='direccion' value = '" + sucursal.First().Direccion + "' class='form-control'/>" +
                             "</div>" +
                              "<div class='form-group'>" +
                             "<label for='EstadoId' class='control-label'>Estado</label>" +
                             "<select for ='EstadoId' id='EstadoId' class='form-control'>" +
                             ddlEstado +
                             "</select>" +
                             "</div>" +
                             "<div class='form-group'>" +
                             "<label for='EstadoId' class='control-label'> Pais</label>" +
                             "<select for ='EstadoId' id='EstadoId' class='form-control'>" +
                              ddlPais +
                             "</select>" +
                             "</div>" +
                             "<div class='form-group'>" +
                             "<button class='btn btn-success' onclick='guardarSucursal(1," + sucursal.First().SucursalesId + ")'> Agregar </button>" +
                             "</div>" +
                             "</form>" +
                             "</div>" +
                             "</div>";



            }

            else if (funcion == 3)//detalles
            {

            }

            else if (funcion == 4)//borrar
            {

            }


            Object[] dataObj = { dataFilter };
            data.Add(dataObj);

            return data;

        }



        // GET: Sucursales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursales = await _context.Sucursales
                .Include(s => s.Empresas)
                .Include(s => s.Paises)
                .SingleOrDefaultAsync(m => m.SucursalesId == id);
            if (sucursales == null)
            {
                return NotFound();
            }

            return View(sucursales);
        }

        // GET: Sucursales/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId");
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "EstadoId");
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId");
            return View();
        }

        // POST: Sucursales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucursalesId,Nombre,Direccion,Telefono,Estatus,PaisId,EstadoId,EmpresaId,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Sucursales sucursales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sucursales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", sucursales.EmpresaId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", sucursales.PaisId);
            return View(sucursales);
        }

        // GET: Sucursales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursales = await _context.Sucursales.SingleOrDefaultAsync(m => m.SucursalesId == id);
            if (sucursales == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", sucursales.EmpresaId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", sucursales.PaisId);
            return View(sucursales);
        }

        // POST: Sucursales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SucursalesId,Nombre,Direccion,Telefono,Estatus,PaisId,EstadoId,EmpresaId,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Sucursales sucursales)
        {
            if (id != sucursales.SucursalesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursalesExists(sucursales.SucursalesId))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", sucursales.EmpresaId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", sucursales.PaisId);
            return View(sucursales);
        }

        // GET: Sucursales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursales = await _context.Sucursales
                .Include(s => s.Empresas)
                .Include(s => s.Paises)
                .SingleOrDefaultAsync(m => m.SucursalesId == id);
            if (sucursales == null)
            {
                return NotFound();
            }

            return View(sucursales);
        }

        // POST: Sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sucursales = await _context.Sucursales.SingleOrDefaultAsync(m => m.SucursalesId == id);
            _context.Sucursales.Remove(sucursales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SucursalesExists(int id)
        {
            return _context.Sucursales.Any(e => e.SucursalesId == id);
        }
    }
}
