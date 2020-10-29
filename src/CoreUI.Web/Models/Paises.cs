using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Web.Models
{
    public class Paises
    {
        [Key]
        public int PaisId { get; set; }
        public string Nombre { get; set; }
        public Boolean Estatus { get; set; }

        // relaciones
        public ICollection<Estados> Estados { get; set; }
        public ICollection<Empresas> Empresas { get; set; }
        public ICollection<Sucursales> Sucursales { get; set; }
    }
}
