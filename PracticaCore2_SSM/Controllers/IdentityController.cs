using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaCore2_SSM.Models;
using PracticaCore2_SSM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Controllers {
    public class IdentityController : Controller {

        LibrosRepository repository;

        public IdentityController (LibrosRepository repo) { this.repository = repo; }
        public async Task<IActionResult> Login () {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (String user, String password) {
            Usuario usuario = this.repository.GetUsuario(user, password);
            if(usuario == null) {
                ViewData["Mensaje"] = "Usuario/Contraseña incorrectos";
                return View();
            } else {
                ClaimsIdentity identity = new ClaimsIdentity
                    (CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre.ToString()));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                   principal, new AuthenticationProperties {
                       IsPersistent = true,
                       ExpiresUtc = DateTime.Now.AddMinutes(20)
                   });
                String action = TempData["action"].ToString();
                String controller = TempData["controller"].ToString();

                return RedirectToAction(action, controller);
            }
        }

        public async Task<IActionResult> Logout () {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
