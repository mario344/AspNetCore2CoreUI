using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreUI.Web.Data;
using CoreUI.Web.Models;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;

namespace CoreUI.Web.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string saludos = "";

        public HabitacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Habitaciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Habitaciones.ToListAsync());
        }


        public List<Object[]>ListarHabitaciones(int pagina, string busqueda)
        {

            //int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 15;
            //int can_paginas;
            //string dataFilter = "", paginador = "", Estado = null, tipo = null, boton = ""; ;
            string dataFilter = "", estatusHab ="";
            List<object[]> data = new List<object[]>();

            // iniciamos con linq de c#

            // forma facil de consulta a una sola tabla
            //var query1 = _context.Paises.OrderBy(c => c.Nombre).Where(c => c.Estatus == true).Select(c => c.Nombre).ToList();

            // para dos tablas
            //var query2 = (from p in _context.Paises.ToList()
            //             join e in _context.Estados
            //             on p.PaisId equals e.PaisId
            //             join em in _context.Empresas
            //             on p.PaisId equals em.PaisId
            //             where p.Estatus == true
            //             select new
            //             {
            //                 p.PaisId,
            //                 p.Nombre,
            //                 p.Estatus,
            //                 idpaisEstado = e.PaisId,
            //                 e.EstadoId,
            //                 NombreEstado = e.Nombre,
            //                 em.Telefono

            //             }).OrderBy(c => c.NombreEstado) ;

            var query = (from h in _context.Habitaciones.ToList()
                         join th in _context.TiposHabitacion
                         on h.TiposHabitacionID equals th.TiposHabitacionID
                         join p in _context.Pisos
                         on h.PisoId equals p.PisoId
                         select new
                         {
                             h.HabitacionesId,
                             h.Nombre,
                             h.NoHabitacion,
                             h.Estatus,
                             th.NombreCorto,
                             th.PrecioHabitacion,
                             th.PrecioNinioExtra,
                             th.PrecioPersonaExtra,
                             p.Descripcion


                         }).OrderBy(c => c.NoHabitacion);

            foreach (var item in query )
            {
                // condicion para los estatus
                if (item.Estatus == true)
                {
                    estatusHab = "Activo";
                }
                else if (item.Estatus == false)
                {
                    estatusHab = "Inactivo";
                }

                dataFilter += "<tr>" +
                                "<th>" +
                                    item.NombreCorto +
                                "</th>" +
                                "<th>" +
                                    item.Nombre +
                                "</th>" +
                                "<th>" +
                                    item.NombreCorto+
                                "</th>" +
                                "<th>" +
                                    item.PrecioHabitacion +
                                "</th>" +
                                "<th>" +
                                     item.PrecioNinioExtra +
                                "</th>" +
                                "<th>" +
                                    estatusHab +
                                "</th>" +
                                "<th>" +
                                    item.Descripcion +
                                "</th>" +
                                "<th>" +


                                    "<a class='btn btn-success active' data-toggle='modal' data-target='#modalHabitacion' onclick='creaModalHabitaciones(2, " + item.HabitacionesId + ")'> Editar</a> |" +
                                   "<a class='btn btn-info active' data-toggle='modal' data-target='#modalHabitacion' onclick='creaModalHabitaciones(3, " + item.HabitacionesId + ")'> Detalles</a> |" +
                                   "<a class='btn btn-danger active' data-toggle='modal' data-target='#modalHabitacion' onclick='creaModalHabitaciones(4, " + item.HabitacionesId + ")'> Borrar</a>" +


                                "</th> " +
                            "</tr>";

            }

            Object[] dataObj = { dataFilter };
            data.Add(dataObj);

            return data;

        }
       
        public List<Object[]> CreaModalHabitaciones(byte funcion, int id)
        {
            string dataFilter = "", ddlsucursales ="", ddlpisos="", ddlHabitaciones="";
            List<object[]> data = new List<object[]>();


            if (funcion == 1) // crea modal nuevo elemento
            {

                // var sucursalesList = _context.Sucursales.Select(C => C.nombre).ToList();// una columna
                //var sucursalesList = _context.Sucursales.Where(c => c.Estatus == true).ToList() // todas las columnas
                var sucursalList = (from s in _context.Sucursales.ToList()
                                    where s.Estatus == true
                                    select new
                                    {
                                        s.SucursalesId,
                                        s.Nombre
                                    }).OrderBy(c => c.Nombre);

                var pisoList = (from p in _context.Pisos.ToList()
                                where p.Estatus == 1
                                select new

                                {
                                    p.Descripcion,
                                    p.PisoId
                                }).OrderBy(c => c.PisoId);

                var tipoHab = (from h in _context.TiposHabitacion.ToList()
                               where h.Estatus == true
                               select new
                               {
                                   h.TiposHabitacionID,
                                   h.Habitaciones,
                                   h.PrecioNinioExtra,

                               }).OrderBy(c => c.Habitaciones);


                //////////listas

                ddlsucursales = "<option value = '0' > Seleccione una sucursal</option >";
                foreach (var item in sucursalList)
                {
                    ddlsucursales += "<option value = '" + item.SucursalesId + "' > " + item.Nombre + " </option >";
                }
                ddlpisos = "<option value = '0' > Seleccione un piso</option >";
                foreach (var item in pisoList)
                {
                    ddlpisos += "<option value = '" + item.PisoId + "'> " + item.Descripcion + " </option>";
                }

                ddlHabitaciones = "<option value = '0' > Seleccione una sucursal</option >";
                foreach (var item in tipoHab)
                {
                    ddlHabitaciones += "<option value = '" + item.TiposHabitacionID + "'> " + item.Habitaciones + " </option>";
                }



                dataFilter = "<div class='row'>" +
                    "<div class='col-md-4'>" +
                        "<form >           " +
                            "<div class='form-group'>" +
                                "<label for='SucursalesId'  class='control-label'>Sucursal ID</label>" +
                                "<select for='SucursalesId' id='SucursalesId' class='form-control' >" +
                                 ddlsucursales +
                                "</select>" +
                            "</div>" +
                            "<div class='form-group'>" +
                                "<label for='Nombre' class='control-label'>Nombre</label>" +
                                "<input for='Nombre' id='Nombre' class='form-control' />                " +
                            "</div>" +
                            "<div class='form-group'>" +
                                "<label for='NombreCorto' class='control-label'>Nombre Corto</label>" +
                                "<input for='NombreCorto' id='NombreCorto' class='form-control' />               " +
                            "</div>" +
                            "<div class='form-group'>" +
                                "<label for='NoHabitacion' class='control-label'>Número Habitación</label>" +
                                "<input for='NoHabitacion' id='NoHabitacion' class='form-control' />               " +
                            "</div>" +
                            "<div class='form-group'>" +
                                "<label for='ClaveHabitacion' class='control-label'>Clave Habitación</label>" +
                                "<input for='ClaveHabitacion' id='ClaveHabitacion'' class='form-control' />               " +
                            "</div>" +
                            "<div class='form-group'>" +
                                "<label for='EstatusAdminitrador' class='control-label'>Estatus Administrador </label>" +
                                "<input for= 'EstatusAdminitrador' id='EstatusAdminitrador' class='form-control'/>              " +
                            "</div>           " +
                            "<div class='form-group'>" +
                                "<label for='PisoId' class='control-label'>Piso </label>" +
                                "<select for='PisoId' id='PisoId' class='form-control'>" +
                                ddlpisos +
                                 "</select>" +
                            "</div>" +
                            "<div class='form-group'>" +
                                "<label for='PisoId' class='control-label'>Piso </label>" +
                                "<select for='PisoId' id='PisoId' class='form-control'>" +
                                ddlHabitaciones +
                                 "</select>" +
                            "</div>" +

                            "<div class='form-group'>" +
                                "<button class='btn btn-success' onclick = 'guardarHabitacion(1, 0)'>Agregar </button>" +
                            "</div>" +
                        "</form>" +
                    "</div>" +
                "</div>";
            }
            else if (funcion == 2) // editar el registro en el modal
            {
                var habitacion = from h in _context.Habitaciones.ToList()
                                 join s in _context.Sucursales
                                 on h.SucursalesId equals s.SucursalesId
                                 join th in _context.TiposHabitacion
                                 on h.TiposHabitacionID equals th.TiposHabitacionID
                                 join p in _context.Pisos
                                 on h.PisoId equals p.PisoId
                                 where h.HabitacionesId == id

                                 select new
                                 {
                                     h.HabitacionesId,
                                     h.ClaveHabitacion,
                                     h.Estatus,
                                     h.EstatusAdminitrador,
                                     h.NoHabitacion,
                                     h.Nombre,
                                     h.NombreCorto,
                                     h.SucursalesId,
                                     h.TiposHabitacionID,
                                     h.UsrAlta,
                                     h.UsrFechaAlta,
                                     h.UsrFechaMod,
                                     h.UsrMod,
                                     h.PisoId,
                                     sucursalN = s.Nombre,
                                     tHabitacion = th.Nombre,
                                     p.Descripcion

                                 };


                var sucursalList = (from s in _context.Sucursales.ToList()
                                    where s.Estatus == true
                                    select new
                                    {
                                        s.SucursalesId,
                                        s.Nombre
                                    }).OrderBy(c => c.Nombre);

                var pisoList = (from p in _context.Pisos.ToList()
                                where p.Estatus == 1
                                select new

                                {
                                    p.Descripcion,
                                    p.PisoId
                                }).OrderBy(c => c.PisoId);

                var tipoHab = (from h in _context.TiposHabitacion.ToList()
                               where h.Estatus == true
                               select new
                               {
                                   h.TiposHabitacionID,
                                   h.Habitaciones,
                                   h.PrecioNinioExtra,

                               }).OrderBy(c => c.Habitaciones);

                ddlsucursales = "<option value = '" + habitacion.First().SucursalesId + "' selected> " + habitacion.First().sucursalN + "</option >";
                foreach (var item in sucursalList)
                {
                    ddlsucursales += "<option value = '" + item.SucursalesId + "' > " + item.Nombre + " </option >";
                }

                ddlpisos = "<option value = '" + habitacion.First().PisoId + "' selected> " + habitacion.First().Descripcion + "</option >";
                foreach (var item in pisoList)
                {
                    ddlpisos += "<option value = '" + item.PisoId + "'> " + item.Descripcion + " </option>";
                }
                ddlHabitaciones = "<option value = '" + habitacion.First().TiposHabitacionID + "' selected> " + habitacion.First().tHabitacion + "</option >";
                foreach (var item in tipoHab)
                {
                    ddlHabitaciones += "<option value = '" + item.TiposHabitacionID + "'> " + item.Habitaciones + " </option>";
                }

                dataFilter = "<div class='row'>" +
                  "<div class='col-md-4'>" +
                      "<form >           " +
                          "<div class='form-group'>" +
                              "<label for='SucursalesId'  class='control-label'>Sucursal ID</label>" +
                              "<select for='SucursalesId' id='SucursalesId' class='form-control' >" +
                               ddlsucursales +
                              "</select>" +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='Nombre' class='control-label'>Nombre</label>" +
                              "<input for='Nombre' id='Nombre'  value = '"+ habitacion.First().Nombre+"' class='form-control' />                " +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='NombreCorto' class='control-label'>Nombre Corto</label>" +
                              "<input for='NombreCorto' id='NombreCorto' value= '"+habitacion.First().NombreCorto+"' class='form-control' />               " +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='NoHabitacion' class='control-label'>Número Habitación</label>" +
                              "<input for='NoHabitacion' id='NoHabitacion' value= '" + habitacion.First().NoHabitacion + "' class='form-control' />               " +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='ClaveHabitacion' class='control-label'>Clave Habitación</label>" +
                              "<input for='ClaveHabitacion' id='ClaveHabitacion'' value= '" + habitacion.First().ClaveHabitacion + "' class='form-control' />               " +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='EstatusAdminitrador' class='control-label'>Estatus Administrador </label>" +
                              "<input for= 'EstatusAdminitrador' id='EstatusAdminitrador' value= '" + habitacion.First().EstatusAdminitrador + "' class='form-control'/>              " +
                          "</div>           " +
                          "<div class='form-group'>" +
                              "<label for='PisoId' class='control-label'>Piso </label>" +
                              "<select for='PisoId' id='PisoId' class='form-control'>" +
                              ddlpisos +
                               "</select>" +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='PisoId' class='control-label'>Piso </label>" +
                              "<select for='PisoId' id='PisoId' class='form-control'>" +
                              ddlHabitaciones +
                               "</select>" +
                          "</div>" +

                          "<div class='form-group'>" +
                              "<button class='btn btn-success' onclick = 'guardarHabitacion(2, "+habitacion.First().HabitacionesId+" )'>Agregar </button>" +
                          "</div>" +
                      "</form>" +
                  "</div>" +
              "</div>";






            }
            else if (funcion == 3)// detalles del registro modal
            {

                var habitacion = from h in _context.Habitaciones.ToList()
                                 join s in _context.Sucursales
                                 on h.SucursalesId equals s.SucursalesId
                                 join th in _context.TiposHabitacion
                                 on h.TiposHabitacionID equals th.TiposHabitacionID
                                 join p in _context.Pisos
                                 on h.PisoId equals p.PisoId
                                 where h.HabitacionesId == id

                                 select new
                                 {
                                     h.HabitacionesId,
                                     h.ClaveHabitacion,
                                     h.Estatus,
                                     h.EstatusAdminitrador,
                                     h.NoHabitacion,
                                     h.Nombre,
                                     h.NombreCorto,
                                     h.SucursalesId,
                                     h.TiposHabitacionID,
                                     h.UsrAlta,
                                     h.UsrFechaAlta,
                                     h.UsrFechaMod,
                                     h.UsrMod,
                                     h.PisoId,
                                     sucursalN = s.Nombre,
                                     tHabitacion = th.Nombre,
                                     p.Descripcion

                                 };


                var sucursalList = (from s in _context.Sucursales.ToList()
                                    where s.Estatus == true
                                    select new
                                    {
                                        s.SucursalesId,
                                        s.Nombre
                                    }).OrderBy(c => c.Nombre);

                var pisoList = (from p in _context.Pisos.ToList()
                                where p.Estatus == 1
                                select new

                                {
                                    p.Descripcion,
                                    p.PisoId
                                }).OrderBy(c => c.PisoId);

                var tipoHab = (from h in _context.TiposHabitacion.ToList()
                               where h.Estatus == true
                               select new
                               {
                                   h.TiposHabitacionID,
                                   h.Habitaciones,
                                   h.PrecioNinioExtra,

                               }).OrderBy(c => c.Habitaciones);

                ddlsucursales = "<option value = '" + habitacion.First().SucursalesId + "' selected> " + habitacion.First().sucursalN + "</option >";
                foreach (var item in sucursalList)
                {
                    ddlsucursales += "<option value = '" + item.SucursalesId + "' > " + item.Nombre + " </option >";
                }

                ddlpisos = "<option value = '" + habitacion.First().PisoId + "' selected> " + habitacion.First().Descripcion + "</option >";
                foreach (var item in pisoList)
                {
                    ddlpisos += "<option value = '" + item.PisoId + "'> " + item.Descripcion + " </option>";
                }
                ddlHabitaciones = "<option value = '" + habitacion.First().TiposHabitacionID + "' selected> " + habitacion.First().tHabitacion + "</option >";
                foreach (var item in tipoHab)
                {
                    ddlHabitaciones += "<option value = '" + item.TiposHabitacionID + "'> " + item.Habitaciones + " </option>";
                }

                dataFilter = "<div class='row'>" +
                  "<div class='col-md-4'>" +
                      "<form >           " +
                          "<div class='form-group'>" +
                              "<label for='SucursalesId'  class='control-label'>Sucursal ID</label>" +
                              "<select for='SucursalesId' id='SucursalesId' class='form-control' disabled>" +
                               ddlsucursales +
                              "</select>" +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='Nombre' class='control-label'>Nombre</label>" +
                              "<input for='Nombre' id='Nombre'  value = '" + habitacion.First().Nombre + "' class='form-control' disabled/>                " +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='NombreCorto' class='control-label'>Nombre Corto</label>" +
                              "<input for='NombreCorto' id='NombreCorto' value= '" + habitacion.First().NombreCorto + "' class='form-control' disabled/ >               " +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='NoHabitacion' class='control-label'>Número Habitación</label>" +
                              "<input for='NoHabitacion' id='NoHabitacion' value= '" + habitacion.First().NoHabitacion + "' class='form-control' disabled/>               " +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='ClaveHabitacion' class='control-label'>Clave Habitación</label>" +
                              "<input for='ClaveHabitacion' id='ClaveHabitacion'' value= '" + habitacion.First().ClaveHabitacion + "' class='form-control' disabled/>               " +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='EstatusAdminitrador' class='control-label'>Estatus Administrador </label>" +
                              "<input for= 'EstatusAdminitrador' id='EstatusAdminitrador' value= '" + habitacion.First().EstatusAdminitrador + "' class='form-control' disabled/>              " +
                          "</div>           " +
                          "<div class='form-group'>" +
                              "<label for='PisoId' class='control-label'>Piso </label>" +
                              "<select for='PisoId' id='PisoId' class='form-control' disabled>" +
                              ddlpisos +
                               "</select>" +
                          "</div>" +
                          "<div class='form-group'>" +
                              "<label for='PisoId' class='control-label'>Piso </label>" +
                              "<select for='PisoId' id='PisoId' class='form-control' disabled>" +
                              ddlHabitaciones +
                               "</select>" +
                          "</div>" +

                         
                      "</form>" +
                  "</div>" +
              "</div>";




            }
            else if (funcion == 4) // borrar registro
            {
                var habitacion = from h in _context.Habitaciones.ToList()
                                 where h.HabitacionesId == id
                                 select new
                                 {
                                     h.HabitacionesId,
                                     h.Nombre,
                                     h.NoHabitacion
                                 };

                dataFilter = "<H2>¿Desea borrar la habitacion de nombre  "+habitacion.First().Nombre+", con el número "+habitacion.First().NoHabitacion+"  ?</H2>";

                dataFilter += "<div class='form-group'>" +
                             "<button class='btn btn-danger' onclick = 'guardarHabitacion(4, " + habitacion.First().HabitacionesId + ")'>Borrar </button>" +
                         "</div>";
            }

            Object[] dataObj = { dataFilter };
            data.Add(dataObj);

            return data;

        }






        // GET: Habitaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitaciones = await _context.Habitaciones
                .FirstOrDefaultAsync(m => m.HabitacionesId == id);
            if (habitaciones == null)
            {
                return NotFound();
            }

            return View(habitaciones);
        }

        // GET: Habitaciones/Create
        public IActionResult Create()
        {
            ViewData["SucursalesId"] = new SelectList(_context.Sucursales, "SucursalesId", "Nombre");
            ViewData["TiposHabitacionID"] = new SelectList(_context.TiposHabitacion, "TiposHabitacionID", "Nombre");
            ViewData["PisoId"] = new SelectList(_context.Pisos, "PisoId", "Descripcion");
            return View();
        }

        // POST: Habitaciones1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucursalesId,HabitacionesId,TiposHabitacionID,Nombre,NombreCorto,NoHabitacion,ClaveHabitacion,EstatusAdminitrador,Estatus,PisoId,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Habitaciones habitaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(habitaciones);
        }

        // GET: Habitaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitaciones = await _context.Habitaciones.FindAsync(id);
            if (habitaciones == null)
            {
                return NotFound();
            }
            ViewData["SucursalesId"] = new SelectList(_context.Sucursales, "SucursalesId", "Nombre");
            ViewData["TiposHabitacionID"] = new SelectList(_context.TiposHabitacion, "TiposHabitacionID", "Nombre");
            ViewData["PisoId"] = new SelectList(_context.Pisos, "PisoId", "Descripcion");
            return View(habitaciones);
        }

        // POST: Habitaciones1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SucursalesId,HabitacionesId,TiposHabitacionID,Nombre,NombreCorto,NoHabitacion,ClaveHabitacion,EstatusAdminitrador,Estatus,PisoId,UsrAlta,UsrFechaAlta,UsrMod,UsrFechaMod")] Habitaciones habitaciones)
        {
            if (id != habitaciones.HabitacionesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionesExists(habitaciones.HabitacionesId))
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
            return View(habitaciones);
        }

        // GET: Habitaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitaciones = await _context.Habitaciones
                .FirstOrDefaultAsync(m => m.HabitacionesId == id);
            if (habitaciones == null)
            {
                return NotFound();
            }

            return View(habitaciones);
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitaciones = await _context.Habitaciones.FindAsync(id);
            _context.Habitaciones.Remove(habitaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionesExists(int id)
        {
            return _context.Habitaciones.Any(e => e.HabitacionesId == id);
        }
    }
}
