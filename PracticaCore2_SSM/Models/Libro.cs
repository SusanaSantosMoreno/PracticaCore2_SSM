using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Models {
    [Table("LIBROS")]
    public class Libro {

        [Key]
        [Column("IdLibro")]
        public int IdLibro { get; set; }
        [Column("Titulo")]
        public String Titulo { get; set; }
        [Column("Autor")]
        public String Autor { get; set; }
        [Column("Editorial")]
        public String Editorial { get; set; }
        [Column("Portada")]
        public String Portada { get; set; }
        [Column("Resumen")]
        public String Resumen { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("IdGenero")]
        public int IdGenero { get; set; }

        public Libro () {}

        public Libro (int idLibro, string titulo, string autor, string editorial, 
            string portada, string resumen, int precio, int idGenero) {
            IdLibro = idLibro;
            Titulo = titulo;
            Autor = autor;
            Editorial = editorial;
            Portada = portada;
            Resumen = resumen;
            Precio = precio;
            IdGenero = idGenero;
        }
    }
}
