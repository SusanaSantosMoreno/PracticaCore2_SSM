using Microsoft.AspNetCore.Mvc;
using PracticaCore2_SSM.Filters;
using PracticaCore2_SSM.Helpers;
using PracticaCore2_SSM.Models;
using PracticaCore2_SSM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Controllers {
    [AuthorizeLibros]
    public class UsersController : Controller {
        LibrosRepository repository;
        CachingService cachingService;

        public UsersController (LibrosRepository repo, CachingService cachingService) {
            this.repository = repo;
            this.cachingService = cachingService;
        }

        public IActionResult Perfil () {

            return View();
        }

        public IActionResult GetPerfilPartial () {
            Usuario usuario = this.repository.GetUsuario
                (int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString()));
            return PartialView("_PerfilPartial", usuario);
        }

        public IActionResult GetPedidosPartial () {
            Usuario usuario = this.repository.GetUsuario
                (int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString()));
            List<VistaPedidos> pedidos = this.repository.GetPedidosUsuario(usuario.IdUsuario);
            return PartialView("_PedidosPartial", pedidos);
        }
    }
}
