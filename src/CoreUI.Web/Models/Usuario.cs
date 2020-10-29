using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Web.Models
{
    public class Usuario
    {
        
        public virtual DateTimeOffset? LockoutEnd { get; set; }
     
        public virtual bool TwoFactorEnabled { get; set; }
 
        public virtual bool PhoneNumberConfirmed { get; set; }
      
        public virtual string PhoneNumber { get; set; }
       
        public virtual string ConcurrencyStamp { get; set; }
    
        public virtual string SecurityStamp { get; set; }
      
        public virtual string PasswordHash { get; set; }
       
        public virtual bool EmailConfirmed { get; set; }
       
        public virtual string NormalizedEmail { get; set; }
    
        public virtual string Email { get; set; }
     
        public virtual string NormalizedUserName { get; set; }
    
        public virtual string UserName { get; set; }
   
        public virtual string Id { get; set; }
      
        public virtual bool LockoutEnabled { get; set; }
      
        public virtual int AccessFailedCount { get; set; }

        public byte UsrSuperAdminMax { get; set; }
        public byte UsuarioAdministrador { get; set; }

        // campos de auditoria
        [StringLength(256)]
        public string UsrAlta { get; set; }
        public DateTime UsrFechaAlta { get; set; }
        [StringLength(256)]
        public string UsrMod { get; set; }
        public DateTime UsrFechaMod { get; set; }


        public string Role { get; set; }
        public string RoleId { get; set; }

    }
}
