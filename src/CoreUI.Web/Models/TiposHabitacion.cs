using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreUI.Web.Models
{
    public class TiposHabitacion
    {
        public int TiposHabitacionID { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public String Nombre { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public String NombreCorto { get; set; }
        [StringLength(300, MinimumLength = 3)]
        public string Observaciones { get; set; }
        [Range(0,99)]
        public byte NoPesonas { get; set; }
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 999999999.99)]
        public decimal PrecioHabitacion { get; set; }
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 999999999.99)]
        public decimal PrecioPersonaExtra { get; set; }
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 999999999.99)]
        public decimal PrecioNinioExtra { get; set; }
        public bool Estatus { get; set; }
        public int SucursalesId { get; set; }

        // campos de auditoria
        [StringLength(60, MinimumLength = 3)]
        public string UsrAlta { get; set; }
        public DateTime UsrFechaAlta { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string UsrMod { get; set; }
        public DateTime UsrFechaMod { get; set; }

        public ICollection<Habitaciones> Habitaciones { get; set; }

    }
}
