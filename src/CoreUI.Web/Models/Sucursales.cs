using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Web.Models
{
    public class Sucursales
    {
        [Key]
        public int SucursalesId { get; set; }
        [StringLength(256)] 
        public string Nombre { get; set; }
        [StringLength(300)]
        public string Direccion { get; set; }
        [StringLength(50)]
        public string Telefono { get; set; }

        public Boolean Estatus { get; set; }
        [ForeignKey("PaisID")]
        public int PaisId { get; set; }
        public Paises Paises { get; set; }

        [ForeignKey("EstadoId")]
        public int EstadoId { get; set; }


        [ForeignKey("EmpresaId")]
        public int EmpresaId { get; set; }
        public Empresas Empresas { get; set; }

        // campos de auditoria
        [StringLength(60, MinimumLength = 3)]
        public string UsrAlta { get; set; }
        public DateTime UsrFechaAlta { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string UsrMod { get; set; }
        public DateTime UsrFechaMod { get; set; }

        public ICollection<TiposHabitacion> TiposHabitacions { get; set; }
        public ICollection<Habitaciones> Habitaciones { get; set; }
        public ICollection<Zonas> Zonas{ get; set; }
        public ICollection<TiposIngresos> TiposIngresos { get; set; }
        public ICollection<Ingresos> Ingresos { get; set; }
        public ICollection<Egresos> Egresos { get; set; }
        public ICollection<TipoEgresos> TipoEgresos { get; set; }

       

    }
}
