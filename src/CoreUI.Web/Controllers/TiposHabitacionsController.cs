using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreUI.Web.Data;
using CoreUI.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace CoreUI.Web.Controllers
{
    public class TiposHabitacionsController : Controller
    {
        private readonly ApplicationDbContext _context;       
        private List<IdentityError> errorList = new List<IdentityError>();
        private string code = "", des = "";
        private readonly UserManager<ApplicationUser> _userManager;

        public TiposHabitacionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;

        }

        // GET: TiposHabitacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposHabitacion.ToListAsync());
        }

        // extrae los tipos de habitacion
        public List<Object[]>RecuperaTipoHabitacion(int numPagina, string valor, string order)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 15;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null, tipo = null, boton = ""; ;
            List<object[]> data = new List<object[]>();
            
            var query = from t in _context.TiposHabitacion.AsNoTracking()
                        join s in _context.Sucursales
                        on t.SucursalesId equals s.SucursalesId
                        select new
                        {
                            s.SucursalesId,
                            s.Nombre,
                            t.TiposHabitacionID,
                            nombrehab = t.Nombre,
                            t.NombreCorto,
                            t.NoPesonas,
                            t.PrecioHabitacion,
                            t.PrecioPersonaExtra,
                            t.PrecioNinioExtra,
                            t.Observaciones,
                            t.Estatus
                        };
            if (query.Count() > 0 )
            {
                foreach (var item in query)
                {
                    count += 1;
                    if (item.Estatus == true )
                    {
                        Estado = "<span class='badge badge-pill badge-success'>Activo</span>";
                    }
                    else
                    {
                        Estado = "<span class='badge badge-pill badge-danger'>Inactivo</span>";
                    }

                    dataFilter += "<tr> " +
                                    "<td> " +
                                        + count +
                                    "</td> " +
                                    "<td> " +
                                        item.Nombre +
                                    "</td> " +
                                    "<td> " +
                                        item.nombrehab +
                                    "</td> " +
                                    "<td> " +
                                        item.NombreCorto +
                                    "</td> " +
                                    "<td> " +
                                        item.NoPesonas +
                                    "</td> " +
                                    "<td> " +
                                        "$ " +item.PrecioHabitacion +
                                    "</td> " +
                                    "<td> " +
                                        "$ " + item.PrecioPersonaExtra +
                                    "</td> " +
                                    "<td> " +
                                        "$ " + item.PrecioNinioExtra +
                                    "</td> " +
                                    "<td> " +
                                        item.Observaciones +
                                    "</td> " +
                                    "<td> " +
                                        Estado +
                                    "</td> " +                                    
                                    "<td> " +
                                        "<a data-toggle='modal' data-target='#modalTipohabitacion'  onclick='adminModalTipoHabitacion(" + 2 + ", " + item.TiposHabitacionID + "," + item.SucursalesId + ")' class='btn btn-success btn-sm' >Editar</a> | " +
                                        "<a  data-toggle='modal' data-target='#modalTipohabitacion'  onclick='adminModalTipoHabitacion(" + 3 + ", " + item.TiposHabitacionID + ", " + item.SucursalesId + ")' class='btn btn-info btn-sm'>Detalles</a> " +                                       
                                    "</td> " +
                                "</tr> ";
                }
            }
            else
            {
                dataFilter = "No existen registros";
            } 

            Object[] dataObj = { dataFilter };
            data.Add(dataObj);

            return data;
        }

        // altas, bajas ymodificacion tipo habitacion
        public List<IdentityError> AdministraTipoHab(int funcion, int id, int idSucursal, string nombre, string nombreCorto, 
            byte NoPersonas, decimal precioHabitacion, decimal precioNinio, decimal precioExtra, string observaciones, int estatus )
        {
            var Estatusbool = false;
            if (estatus == 1)
            {
                Estatusbool = true;
            }
            else
            {
                Estatusbool = false;
            }
            string usuario = _userManager.GetUserName(User);
                                   
            try
            {
                
                if (funcion == 1)
                {
                    var tipoHabitacion = new TiposHabitacion
                    {
                        Estatus = true,
                        NoPesonas = NoPersonas,
                        Nombre = nombre,
                        NombreCorto = nombreCorto,
                        Observaciones = observaciones,
                        PrecioHabitacion = precioHabitacion,
                        PrecioNinioExtra = precioNinio,
                        PrecioPersonaExtra = precioExtra,
                        SucursalesId = idSucursal,
                        UsrAlta = usuario,
                        UsrFechaAlta = DateTime.Now,
                        UsrMod = "",
                        UsrFechaMod = DateTime.Now
                    };
                    _context.Add(tipoHabitacion);
                    _context.SaveChanges();
                    code = "save";
                    des = "save";

                }
                else if (funcion ==2)
                {
                    //var tipohabitacionlist = _context.TiposHabitacion.FirstOrDefault(c => c.TiposHabitacionID == id);
                    var tipohabitacionlist = _context.TiposHabitacion.Where(c => c.TiposHabitacionID == id).AsNoTracking().ToList();


                    var tipoHabitacion = new TiposHabitacion
                    {
                        TiposHabitacionID = tipohabitacionlist[0].TiposHabitacionID,
                        Estatus = Estatusbool,
                        NoPesonas = NoPersonas,
                        Nombre = nombre,
                        NombreCorto = nombreCorto,
                        Observaciones = observaciones,
                        PrecioHabitacion = precioHabitacion,
                        PrecioNinioExtra = precioNinio,
                        PrecioPersonaExtra = precioExtra,
                        SucursalesId = idSucursal,
                        UsrAlta = tipohabitacionlist[0].UsrAlta,
                        UsrFechaAlta =tipohabitacionlist[0].UsrFechaAlta,
                        UsrMod = usuario,
                        UsrFechaMod = DateTime.Now
                    };
                    _context.Update(tipoHabitacion);
                    _context.SaveChanges();
                    code = "save";
                    des = "save";
                }

            }
            catch (Exception e)
            {

                code = "error";
                des = e.Message;
            }
            errorList.Add(new IdentityError
            {
                Code = code,
                Description = des

            });
            return errorList;

        }

        // crea modal tipo habitacion
        public List<Object[]> ModalTipohabitacion( int funcion, int id, int claveSucursal)
        {
            List<object[]> data = new List<object[]>();
            string dataFilter = "";
            bool estatus = false;
            var sucursalesList = _context.Sucursales.OrderByDescending(c => c.Estatus == true).AsNoTracking().ToList();
            var selectSucursales = "";
            if (sucursalesList.Count > 0)
            {
                foreach (var item in sucursalesList)
                {
                    selectSucursales += " <option value='" + item.SucursalesId + "'>" + item.Nombre + "</option> ";
                }
            }

            if (funcion == 1)
            {
                dataFilter = "<div class='modal-header'> " +
                          "<h4 id='titleCategoria' class='modal-title'>Alta tipo habitación</h4> " +
                          "<button type='button' class='close' data-dismiss='modal' aria-label='Close'> " +
                          "<span aria-hidden='true'>&times;</span> " +
                          "</button> " +
                          "</div> " +
                          "<div class='modal-body'> " +

                          "<div class='card'> " +
                          "<div class='card-header'> " +
                          "Crear tipo haabitación" +
                          "</div> " +
                          "<div class='card-body'> " +
                              "<form> " +

                                "<div class='col-md-10'> " +
                                "<select id='sucursalId' form='sucursalId' class='form-control' > " +
                                "<option value='0'>Seleccione una sucursal</option> " +
                                selectSucursales +
                                "</select> " +
                                "</div> " +


                                  "<div class='form-group'> " +
                                  "<label for='Descripcion' class='control-label'>Tipo Habitación</label> " +
                                  "<input for='nombre' id='nombre' class='form-control' /> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Descripcion' class='control-label'>Nombre corto</label> " +
                                  "<input for='nombre_c' id='nombre_c' class='form-control' /> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Número personas</label> " +
                                  "<input type='number' for='nPersonas' id='nPersonas' class='form-control'  min='1' max='100' /> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio habitación</label> " +
                                  "<input type='number' for='precioHabitacion' id='precioHabitacion' class='form-control'  min='0' max='10000' step='.10'/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio por niño</label> " +
                                  "<input type='number' for='precioNinio' id='precioNinio' class='form-control'  min='0' max='10000' step='.10'/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio adulto extra</label> " +
                                  "<input type='number' for='precioAdulto' id='precioAdulto' class='form-control'  min='0' max='10000' step='.10'/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Observaciones</label> " +
                                  "<input for='observaciones' id='observaciones' class='form-control' /> " +
                                  "</div> " +

                                  "<div class='col-md-12'> " +
                                  "<span id = 'mensaje' class='badge badge-danger'></span> " +
                                  "</div> " +
                              "</form> " +
                          "</div> " +
                          "</div> " +
                          "</div> " +
                          "<div class='modal-footer'> " +
                          "<button type='button' onclick='altaTipoHabitacion(1, 0)' class='btn btn-primary'>Guardar</button> " +
                          "<button type='button' class='btn btn-default' data-dismiss='modal'>Cerrar</button> " +
                          "</div> ";
            }
            else if (funcion == 2)
            {
               
                var sucursalSeleccion = sucursalesList.FirstOrDefault(c => c.SucursalesId == claveSucursal);
                var tipoHabitacion = _context.TiposHabitacion.FirstOrDefault(c => c.TiposHabitacionID == id);

                estatus = sucursalSeleccion.Estatus;
                var estatusSelected = "";
                if (estatus == true)
                {
                    estatusSelected = " <option value='" + 1 + "' selected>" + "Activo" + "</option> " +
                         "<option value = '" + 0 + "'> " + "Inactivo" + " </option > ";
                }
                else
                {
                    estatusSelected = " <option value='" + 0 + "' selected>" + "Inactivo" + "</option> " +
                    "<option value = '" + 1 + "' > " + "Activo" + " </option > ";
                }                

                dataFilter = "<div class='modal-header'> " +
                          "<h4 id='titleCategoria' class='modal-title'>Editar tipo habitación</h4> " +
                          "<button type='button' class='close' data-dismiss='modal' aria-label='Close'> " +
                          "<span aria-hidden='true'>&times;</span> " +
                          "</button> " +
                          "</div> " +
                          "<div class='modal-body'> " +

                          "<div class='card'> " +
                          "<div class='card-header'> " +
                          "Editar tipo haabitación" +
                          "</div> " +
                          "<div class='card-body'> " +
                              "<form> " +

                                "<div class='col-md-10'> " +
                                "<select id='sucursalId' form='sucursalId' class='form-control' > " +
                                "<option value='" + sucursalSeleccion.SucursalesId + "' selected>" + sucursalSeleccion.Nombre + "</option> " +
                                selectSucursales +
                                "</select> " +
                                "</div> " +


                                  "<div class='form-group'> " +
                                  "<label for='Descripcion' class='control-label' >Tipo Habitación</label> " +
                                  "<input for='nombre' id='nombre' class='form-control' value='" + tipoHabitacion.Nombre + "' /> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Descripcion' class='control-label'>Nombre corto</label> " +
                                  "<input for='nombre_c' id='nombre_c' class='form-control' value='" + tipoHabitacion.NombreCorto + "' /> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Número personas</label> " +
                                  "<input type='number' for='nPersonas' id='nPersonas' class='form-control'  min='1' max='100' value='" + tipoHabitacion.NoPesonas + "' /> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio habitación</label> " +
                                  "<input type='number' for='precioHabitacion' id='precioHabitacion' class='form-control' value='" + tipoHabitacion.PrecioHabitacion + "' min='0' max='10000' step='.10'/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio por niño</label> " +
                                  "<input type='number' for='precioNinio' id='precioNinio' class='form-control' value='" + tipoHabitacion.PrecioNinioExtra + "'  min='0' max='10000' step='.10'/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio adulto extra</label> " +
                                  "<input type='number' for='precioAdulto' id='precioAdulto' class='form-control' value='" + tipoHabitacion.PrecioPersonaExtra + "'  min='0' max='10000' step='.10'/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Observaciones</label> " +
                                  "<input for='observaciones' id='observaciones' class='form-control' value='" + tipoHabitacion.Observaciones + "' /> " +
                                  "</div> " +
                                   "<div class='form-group'> " +
                                   "<label for='Cap' class='control-label'>Estatus</label> " +
                                   "<select id='estatus' name='SelectEstatus' class='form-control'> " +
                                    estatusSelected +
                                   "</select> " +
                                   "</div> " +

                                  "<div class='col-md-12'> " +
                                  "<span id = 'mensaje' class='badge badge-danger'></span> " +
                                  "</div> " +
                              "</form> " +
                          "</div> " +
                          "</div> " +
                          "</div> " +
                          "<div class='modal-footer'> " +
                          "<button type='button' onclick='altaTipoHabitacion(2, "+tipoHabitacion.TiposHabitacionID+")' class='btn btn-primary'>Guardar</button> " +
                          "<button type='button' class='btn btn-default' data-dismiss='modal'>Cerrar</button> " +
                          "</div> ";
            }
            else if (funcion == 3)
            {
                var sucursalSeleccion = sucursalesList.FirstOrDefault(c => c.SucursalesId == claveSucursal);
                var tipoHabitacion = _context.TiposHabitacion.FirstOrDefault(c => c.TiposHabitacionID == id);

                estatus = sucursalSeleccion.Estatus;
                var estatusSelected = "";
                if (estatus == true)
                {
                    estatusSelected = " <option value='" + 1 + "' selected>" + "Activo" + "</option> " +
                         "<option value = '" + 0 + "'> " + "Inactivo" + " </option > ";
                }
                else
                {
                    estatusSelected = " <option value='" + 0 + "' selected>" + "Inactivo" + "</option> " +
                    "<option value = '" + 1 + "' > " + "Activo" + " </option > ";
                }
                dataFilter = "<div class='modal-header'> " +
                          "<h4 id='titleCategoria' class='modal-title'>Editar tipo habitación</h4> " +
                          "<button type='button' class='close' data-dismiss='modal' aria-label='Close'> " +
                          "<span aria-hidden='true'>&times;</span> " +
                          "</button> " +
                          "</div> " +
                          "<div class='modal-body'> " +

                          "<div class='card'> " +
                          "<div class='card-header'> " +
                          "Editar tipo haabitación" +
                          "</div> " +
                          "<div class='card-body'> " +
                              "<form> " +

                                "<div class='col-md-10'> " +
                                "<select id='sucursalId' form='sucursalId' class='form-control'  disabled> " +
                                "<option value='" + sucursalSeleccion.SucursalesId + "' selected>" + sucursalSeleccion.Nombre + "</option> " +
                                selectSucursales +
                                "</select> " +
                                "</div> " +


                                  "<div class='form-group'> " +
                                  "<label for='Descripcion' class='control-label' >Tipo Habitación</label> " +
                                  "<input for='nombre' id='nombre' class='form-control' value='" + tipoHabitacion.Nombre + "'  disabled/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Descripcion' class='control-label'>Nombre corto</label> " +
                                  "<input for='nombre_c' id='nombre_c' class='form-control' value='" + tipoHabitacion.NombreCorto + "'  disabled/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Número personas</label> " +
                                  "<input type='number' for='nPersonas' id='nPersonas' class='form-control'  min='1' max='100' value='" + tipoHabitacion.NoPesonas + "'  disabled/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio habitación</label> " +
                                  "<input type='number' for='precioHabitacion' id='precioHabitacion' class='form-control' value='" + tipoHabitacion.PrecioHabitacion + "' min='0' max='10000' step='.10'  disabled/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio por niño</label> " +
                                  "<input type='number' for='precioNinio' id='precioNinio' class='form-control' value='" + tipoHabitacion.PrecioNinioExtra + "'  min='0' max='10000' step='.10'  disabled/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Precio adulto extra</label> " +
                                  "<input type='number' for='precioAdulto' id='precioAdulto' class='form-control' value='" + tipoHabitacion.PrecioPersonaExtra + "'  min='0' max='10000' step='.10'  disabled/> " +
                                  "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Observaciones</label> " +
                                  "<input for='observaciones' id='observaciones' class='form-control' value='" + tipoHabitacion.Observaciones + "'  disabled/> " +
                                  "</div> " +
                                   "<div class='form-group'> " +
                                   "<label for='Cap' class='control-label'>Estatus</label> " +
                                   "<select id='estatus' name='SelectEstatus' class='form-control'  disabled> " +
                                    estatusSelected +
                                   "</select> " +
                                   "</div> " +

                                   "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Usuario alta</label> " +
                                  "<input for='observaciones' id='observaciones' class='form-control' value='" + tipoHabitacion.UsrAlta + "'  disabled/> " +
                                  "</div> " +
                                  "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Fecha Alta</label> " +
                                  "<input for='observaciones' id='observaciones' class='form-control' value='" + tipoHabitacion.UsrFechaAlta + "'  disabled/> " +
                                  "</div> " +
                                  "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Usuario modificación</label> " +
                                  "<input for='observaciones' id='observaciones' class='form-control' value='" + tipoHabitacion.UsrMod + "'  disabled/> " +
                                  "</div> " +
                                  "<div class='form-group'> " +
                                  "<label for='Cap' class='control-label'>Fecha modificación</label> " +
                                  "<input for='observaciones' id='observaciones' class='form-control' value='" + tipoHabitacion.UsrFechaMod + "'  disabled/> " +
                                  "</div> " +

                                  "<div class='col-md-12'> " +
                                  "<span id = 'mensaje' class='badge badge-danger'></span> " +
                                  "</div> " +
                              "</form> " +
                          "</div> " +
                          "</div> " +
                          "</div> " +
                          "<div class='modal-footer'> " +                          
                          "<button type='button' class='btn btn-default' data-dismiss='modal'>Cerrar</button> " +
                          "</div> ";
            }

            

            Object[] dataObj = { dataFilter };
            data.Add(dataObj);

            return data;

        }

    }
}
