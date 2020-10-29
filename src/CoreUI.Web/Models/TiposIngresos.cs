using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreUI.Web.Models
{
    public class TiposIngresos
    {
        public int SucursalesId { get; set; }
        public int TiposIngresosId { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Nombre { get; set; }
        public bool Estatus { get; set; }

        // campos de auditoria
        [StringLength(60, MinimumLength = 3)]
        public string UsrAlta { get; set; }
        public DateTime UsrFechaAlta { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string UsrMod { get; set; }
        public DateTime UsrFechaMod { get; set; }

        public ICollection<Ingresos> Ingresos { get; set; }

    }
}
