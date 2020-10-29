using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Web.Models
{
    public class Zonas
    {
        public int ZonasId { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public int SucursalesId { get; set; }

        // campos de auditoria
        [StringLength(60, MinimumLength = 3)]
        public string UsrAlta { get; set; }
        public DateTime UsrFechaAlta { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string UsrMod { get; set; }
        public DateTime UsrFechaMod { get; set; }
    }
}
