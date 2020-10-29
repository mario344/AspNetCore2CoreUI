using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Web.Models
{
    public class Pisos
    {
        [Key]
        public int PisoId { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Descripcion { get; set; }
        public byte Estatus { get; set; }

    }
}
