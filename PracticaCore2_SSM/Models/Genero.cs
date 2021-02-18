using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Models {
    [Table("GENEROS")]
    public class Genero {

        [Key]
        [Column("IdGenero")]
        public int IdGenero { get; set; }
        [Column("Nombre")]
        public String Nombre { get; set; }

        public Genero () {}

        public Genero (int idGenero, string nombre) {
            IdGenero = idGenero;
            Nombre = nombre;
        }
    }
}
