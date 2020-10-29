using System;
using System.ComponentModel.DataAnnotations;

namespace CoreUI.Web.Models
{
    public class Egresos
    {
        public int SucursalesId { get; set; }
        public int EgresosId { get; set; }
        public int TipoEgresosId { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Descripcion { get; set; }
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 999999999.99)]
        public decimal Cantidad { get; set; }
        public byte Ajuste { get; set; }
        public byte Cancelado { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string MotivoAjusteCancelacion { get; set; }

        // campos de auditoria
        [StringLength(60, MinimumLength = 3)]
        public string UsrAlta { get; set; }
        public DateTime UsrFechaAlta { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string UsrMod { get; set; }
        public DateTime UsrFechaMod { get; set; }
    }
}
