using System;
using System.ComponentModel.DataAnnotations;

namespace CoreUI.Web.Models
{
    public class Habitaciones
    {
        public int SucursalesId { get; set; }
        public int HabitacionesId { get; set; }
        public int TiposHabitacionID { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Nombre { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public String NombreCorto { get; set; }
        public int NoHabitacion { get; set; }
        [StringLength(10, MinimumLength = 3)]
        public string ClaveHabitacion { get; set; }
        public byte EstatusAdminitrador { get; set; }
        public bool Estatus { get; set; }
        public int PisoId { get; set; }


        // campos de auditoria
        [StringLength(60, MinimumLength = 3)]
        public string UsrAlta { get; set; }
        public DateTime UsrFechaAlta { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string UsrMod { get; set; }
        public DateTime UsrFechaMod { get; set; }
    }
}
