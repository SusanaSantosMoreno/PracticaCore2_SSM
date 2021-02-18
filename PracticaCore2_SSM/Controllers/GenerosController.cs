using Microsoft.AspNetCore.Mvc;
using PracticaCore2_SSM.Models;
using PracticaCore2_SSM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Controllers {
    public class GenerosController : Controller {

        LibrosRepository repository;
        
        public GenerosController(LibrosRepository repo) { this.repository = repo; }

        public IActionResult GetGeneros () {
            List<Genero> generos = this.repository.GetGeneros();
            return PartialView("_GenerosPartial", generos);
        }
    }
}
