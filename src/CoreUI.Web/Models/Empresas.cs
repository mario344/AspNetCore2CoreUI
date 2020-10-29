using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Web.Models
{
    public class Empresas
    {
        [Key]
        public int EmpresaId { get; set; }
        [StringLength(256)]
        public string Nombre { get; set; }
        [StringLength(256)]
        public string Direccion { get; set; }
        [StringLength(50)]
        public string Telefono { get; set; }

        public Boolean Estatus { get; set; }

        [ForeignKey("PaisID")]
        public int PaisId { get; set; }
        public Paises Paises { get; set; }

        [ForeignKey("EstadoId")]
        public int EstadoId { get; set; }
        public Estados Estados { get; set; }

        [ForeignKey("UsuarioId")]
        public int Id { get; set; }

        // campos de auditoria
        public string UsrAlta { get; set; }
        public DateTime UsrFechaAlta { get; set; }
        public string UsrMod { get; set; }
        public DateTime UsrFechaMod { get; set; }
        //relaciones
        public ICollection<Sucursales> Sucursales { get; set; }
       
    }
}
