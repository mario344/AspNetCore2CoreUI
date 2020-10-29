using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Web.Models
{
    public class SucursalesUsuarios
    {
        public string Id { get; set; }
        public int SucursalesId { get; set; }
        public Usuario Usuario { get; set; }
        public Sucursales Sucursales { get; set; }
    }
}
