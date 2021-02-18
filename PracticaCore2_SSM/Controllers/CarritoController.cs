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
    public class CarritoController : Controller {

        LibrosRepository repository;
        CachingService cachingService;

        public CarritoController (LibrosRepository repo, CachingService cachingService) {
            this.repository = repo;
            this.cachingService = cachingService;
        }
        public IActionResult Index () {
            List<int> carrito = this.cachingService.GetCarrito();
            List<Libro> librosCache = new List<Libro>();
            int precioFinal = 0;
            foreach(int lib in carrito) {
                Libro libro = this.repository.GetLibro(lib);
                librosCache.Add(libro);
                precioFinal += libro.Precio;
            }
            ViewData["PrecioFinal"] = precioFinal.ToString();
            return View(librosCache);
        }

        [AuthorizeLibros]
        public IActionResult FinalizarCompra (int? quitado) {
            List<int> carrito = new List<int>();
            if (quitado != null) {
                carrito = this.cachingService.QuitarLibro((int)quitado);
            } else {
                carrito = this.cachingService.GetCarrito();
            }
            List<Libro> librosCache = new List<Libro>();
            int precioFinal = 0;
            foreach (int lib in carrito) {
                Libro libro = this.repository.GetLibro(lib);
                librosCache.Add(libro);
                precioFinal += libro.Precio;
            }
            ViewData["PrecioFinal"] = precioFinal.ToString();
            return View(librosCache);
        }

        [AuthorizeLibros]
        [HttpPost]
        public IActionResult FinalizarCompra (int[] libro, int[] cantidad) {
            int idFactura = this.repository.getUltimaFactura() + 1;
            Usuario usuario = this.repository.GetUsuario
                (int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString()));
            //finalizar compra
            for (int i=0; i<libro.Count(); i++) {
                Pedido pedido = this.repository.getUltimoPedido();
                int idPedido = pedido != null ? pedido.IdPedido + 1 : 1;
                this.repository.guardarPedido(libro[i], idPedido, idFactura, cantidad[i], usuario.IdUsuario);
            }
            this.cachingService.CleanCache();
            return RedirectToAction("Index","Home");
        }
    }
}
