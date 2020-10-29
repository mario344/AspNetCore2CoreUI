using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoreUI.Web.Models;

namespace CoreUI.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<SucursalesUsuarios>().HasKey(x => new { x.Id, x.SucursalesId });
        }

        public DbSet<CoreUI.Web.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<CoreUI.Web.Models.Paises> Paises { get; set; }

        public DbSet<CoreUI.Web.Models.Estados> Estados { get; set; }

        public DbSet<CoreUI.Web.Models.Empresas> Empresas { get; set; }

        public DbSet<CoreUI.Web.Models.Sucursales> Sucursales { get; set; }

        public DbSet<CoreUI.Web.Models.SucursalesUsuarios> SucursalesUsuarios { get; set; }

        public DbSet<CoreUI.Web.Models.TiposHabitacion> TiposHabitacion { get; set; }

        public DbSet<CoreUI.Web.Models.Habitaciones> Habitaciones { get; set; }

        public DbSet<CoreUI.Web.Models.Ingresos> Ingresos { get; set; }

        public DbSet<CoreUI.Web.Models.TipoEgresos> TipoEgresos { get; set; }

        public DbSet<CoreUI.Web.Models.Egresos> Egresos { get; set; }

        public DbSet<CoreUI.Web.Models.Pisos> Pisos { get; set; }
    }
}
