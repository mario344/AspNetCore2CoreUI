using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Web.Models
{
    public class Estados
    {
        [Key]
        public int EstadoId { get; set; }
        public string Nombre { get; set; }
        public Boolean Estatus { get; set; }
        [ForeignKey("PaisID")]
        public int PaisId { get; set; }
        public Paises Paises { get; set; }

        // relaciones
        public ICollection<Sucursales> Sucursales { get; set; }
    }
}
