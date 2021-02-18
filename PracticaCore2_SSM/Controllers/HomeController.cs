using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticaCore2_SSM.Helpers;
using PracticaCore2_SSM.Models;
using PracticaCore2_SSM.Repositories;

namespace MvcCore.Controllers
{
    public class HomeController : Controller {

        LibrosRepository repository;
        CachingService cachingService;

        public HomeController (LibrosRepository repo, CachingService cachingService) { 
            this.repository = repo;
            this.cachingService = cachingService;
        }

        public IActionResult Index(int? idGenero, int? libroComprado, int?posicion) {
            List<LibroPag> libros = new List<LibroPag>();
            int registros = 0;
            posicion = posicion == null ? 1 : posicion;
            if (idGenero != null) {
                ViewData["Genero"] = "Genero";
                libros = this.repository.GetLibrosPaginados((int)idGenero);
            } else {
                libros = this.repository.GetLibrosPaginados((int)posicion, ref registros);
            }
            ViewData["registros"] = registros;
            if (libroComprado != null) {
                this.cachingService.GuardarLibroCarrito((int)libroComprado);
            }
            
            return View(libros);
        }

        public IActionResult DetalleLibro(int idLibro) {
            Libro libro = this.repository.GetLibro(idLibro);
            Genero genero = this.repository.GetGenero(libro.IdGenero);
            ViewData["genero"] = genero.Nombre;
            return View(libro);
        }
    }
}
