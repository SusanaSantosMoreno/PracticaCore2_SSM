using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Helpers {
    public class CachingService {

        private IMemoryCache MemoryCache;
        private IHttpContextAccessor httpContext;

        public CachingService(IMemoryCache cache, IHttpContextAccessor accesor) { 
            this.MemoryCache = cache;
            this.httpContext = accesor;
        }

        public void GuardarLibroCarrito(int idLibro) {
            List<int> libros = new List<int>();
            int carrito = 0;
            if (this.MemoryCache.Get("Libros") != null) {
                libros = ToolkitService.DeserializeJsonObject<List<int>>
                    (this.MemoryCache.Get("Libros").ToString());
            }
            libros.Add(idLibro);
            carrito += libros.Count;
            httpContext.HttpContext.Session.SetInt32("carrito", carrito);
            this.MemoryCache.Set("Libros", ToolkitService.SerializeJsonObject(libros));
        }

        public List<int> GetCarrito () {
            List<int> libros = new List<int>();
            if (this.MemoryCache.Get("Libros") != null) {
                libros = ToolkitService.DeserializeJsonObject<List<int>>
                    (this.MemoryCache.Get("Libros").ToString());
            }
            return libros;
        }

        public List<int> QuitarLibro (int idLibro) {
            List<int> libros = new List<int>();
            if (this.MemoryCache.Get("Libros") != null) {
                libros = ToolkitService.DeserializeJsonObject<List<int>>
                    (this.MemoryCache.Get("Libros").ToString());
            }
            libros.RemoveAll(x => x == idLibro);
            httpContext.HttpContext.Session.SetInt32("carrito", libros.Count);
            this.MemoryCache.Set("Libros", ToolkitService.SerializeJsonObject(libros));
            return libros;
        }

        public void CleanCache () {
            this.MemoryCache.Remove("Libros");
            httpContext.HttpContext.Session.Remove("carrito");
        }
    }
}
