using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreUI.Web.Data;
using CoreUI.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace CoreUI.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        UsuarioRole _usuarioRole;
        public List<SelectListItem> usuarioRole;
        



        public UsuariosController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole>roleManager)

        {            
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _usuarioRole = new UsuarioRole();
            usuarioRole = new List<SelectListItem>();
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            // validamos la existencia de todos los roles
            if (User.Identity.IsAuthenticated)
            {
                //validamos la existencia de los roles
                var xRol = await _roleManager.RoleExistsAsync("Administrador");
                if (!xRol)
                {
                    var role = new IdentityRole
                    {
                        Name = "Administrador"
                    };
                    await _roleManager.CreateAsync(role);
                }

                //validamos la existencia de los roles
                xRol = await _roleManager.RoleExistsAsync("Director");
                if (!xRol)
                {
                    var role = new IdentityRole
                    {
                        Name = "Director"
                    };
                    await _roleManager.CreateAsync(role);
                }
                //validamos la existencia de los roles
                xRol = await _roleManager.RoleExistsAsync("Recepcion");
                if (!xRol)
                {
                    var role = new IdentityRole
                    {
                        Name = "Recepcion"
                    };
                    await _roleManager.CreateAsync(role);
                }
                //validamos la existencia de los roles
                xRol = await _roleManager.RoleExistsAsync("AmaLlaves");
                if (!xRol)
                {
                    var role = new IdentityRole
                    {
                        Name = "AmaLlaves"
                    };
                    await _roleManager.CreateAsync(role);
                }
                //validamos la existencia de los roles
                xRol = await _roleManager.RoleExistsAsync("Mantenimiento");
                if (!xRol)
                {
                    var role = new IdentityRole
                    {
                        Name = "Mantenimiento"
                    };
                    await _roleManager.CreateAsync(role);
                }
                //validamos la existencia de los roles
                xRol = await _roleManager.RoleExistsAsync("Restaurante");
                if (!xRol)
                {
                    var role = new IdentityRole
                    {
                        Name = "Restaurante"
                    };
                    await _roleManager.CreateAsync(role);
                }
                xRol = await _roleManager.RoleExistsAsync("Usuario");
                if (!xRol)
                {
                    var role = new IdentityRole
                    {
                        Name = "Usuario"
                    };
                    await _roleManager.CreateAsync(role);
                }
            }
            // declaramos una variable iD inicializamos vacia
            var ID = "";
            //Declaro una variable de tipo list que depende l clase usuario
            List<Usuario> usuario = new List<Usuario>();
            // obtengo todos los registros de la tabla donde almaceno los usuarios 
            // y los almaceno en el objeto
            var appUsuario = await _context.ApplicationUser.ToListAsync();
            foreach (var Data in appUsuario)
            {
                ID = Data.Id;
                usuarioRole = await _usuarioRole.GetRole(_userManager, _roleManager, ID);

                usuario.Add(new Usuario()
                {
                    Id = Data.Id,
                    UserName = Data.UserName,
                    PhoneNumber = Data.PhoneNumber,
                    Email = Data.Email,
                    Role = usuarioRole[0].Text

                });

            }

            return View(usuario.ToList());
            //return View(await _context.ApplicationUser.ToListAsync());
            // return View(await _context.ApplicationUser.ToListAsync());
        }

        // obtener los datos de un usuario especifico
        public async Task<List<Usuario>> GetUsuario (string id)
        {
            // generamos una lista de los suarios aplicacion

            List<Usuario> usuario = new List<Usuario>();
            var appUsuario = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            usuarioRole = await _usuarioRole.GetRole(_userManager, _roleManager, id);
            usuario.Add(new Usuario()
            {
                Id = appUsuario.Id,
                UserName = appUsuario.UserName,
                PhoneNumber = appUsuario.PhoneNumber,
                Email = appUsuario.Email,
                Role = usuarioRole[0].Text,
                RoleId = usuarioRole[0].Value,
                AccessFailedCount = appUsuario.AccessFailedCount,
                ConcurrencyStamp = appUsuario.ConcurrencyStamp,
                EmailConfirmed = appUsuario.EmailConfirmed,
                LockoutEnabled = appUsuario.LockoutEnabled,
                LockoutEnd = appUsuario.LockoutEnd,
                NormalizedEmail = appUsuario.NormalizedEmail,
                NormalizedUserName = appUsuario.NormalizedUserName,
                PasswordHash = appUsuario.PasswordHash,
                PhoneNumberConfirmed = appUsuario.PhoneNumberConfirmed,
                SecurityStamp = appUsuario.SecurityStamp,
                TwoFactorEnabled = appUsuario.TwoFactorEnabled

            });
            return usuario;

        }

        public async Task<List<SelectListItem>> GetRoles()
        {
            List<SelectListItem> roleslista = new List<SelectListItem>();
            roleslista = _usuarioRole.Roles(_roleManager);
            return roleslista;
        }

        public async Task<string> EditUsuario(string id, string userName, string email, string phoneNumber,
            int accessFailedCount, string concurrencyStamp, bool emailConfirmed, bool lockoutEnabled, DateTimeOffset lockoutEnd, string normalizedEmail,
           string normalizedUserName, string passwordHash, bool phoneNumberConfirmed, string securityStamp, bool twoFactorEnabled, string selectRole,
           ApplicationUser applicationUser)
        {

            var resp = "";

            try
            {
                applicationUser = new ApplicationUser
                {
                    Id = id,
                    UserName = userName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    AccessFailedCount = accessFailedCount,
                    ConcurrencyStamp = concurrencyStamp,
                    EmailConfirmed = emailConfirmed,
                    LockoutEnabled = lockoutEnabled,
                    LockoutEnd = lockoutEnd,
                    NormalizedEmail = normalizedEmail,
                    NormalizedUserName = normalizedUserName,
                    PasswordHash = passwordHash,
                    PhoneNumberConfirmed = phoneNumberConfirmed,
                    SecurityStamp = securityStamp,
                    TwoFactorEnabled = twoFactorEnabled
                };
                //actualizar los datos
                _context.Update(applicationUser);
                await _context.SaveChangesAsync();

                // obtener los datos del usuario
                var usuario = await _userManager.FindByIdAsync(id);

                usuarioRole = await _usuarioRole.GetRole(_userManager, _roleManager, id);
                // si el usuario tiene roles
                if (usuarioRole[0].Text != "No Role")
                {
                    await _userManager.RemoveFromRoleAsync(usuario, usuarioRole[0].Text);
                }
                if (usuarioRole[0].Text == "No Role")
                {
                    selectRole = "usuario";
                }

                // ahora si almacenamos el rol
                var resultado = await _userManager.AddToRoleAsync(usuario, selectRole);
                resp = "Save";

                // obtener los datos del usuario
                //var usuario = await _userManager.FindByIdAsync(id);
            }
            catch (Exception e)
            {
                resp = "No save";
                //resp = e.ToString();
            }
            return resp;
        }

        public async Task<String> DeleteUsuario(string id)
        {
            var resp = "";
            try
            {
                var applicactionUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
                _context.ApplicationUser.Remove(applicactionUser);
                await _context.SaveChangesAsync();
                resp = "Delete";
            }
            catch (Exception)
            {

                resp = "Nodelete";
            }
            return resp;

        }


        public async Task<String> CreateUsuario(string email, string phoneNumber, string passwordHash, string selectRole, ApplicationUser applicationUser)
        {
            var resp = "";
            var fecha_servidor = DateTime.Now;
            applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber
               // FechaAlta = fecha_servidor

            };

            var result = await _userManager.CreateAsync(applicationUser, passwordHash);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, selectRole);
                resp = "Save";

            }
            else
            {
                resp = "NoSave";
            }
            return resp;

        }




        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
