using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Models {
    [Table("VISTAPEDIDOS")]
    public class VistaPedidos {

        [Key]
        [Column("IDVISTAPEDIDOS")]
        public int IdVistaPedidos { get; set; }
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("Nombre")]
        public String Nombre { get; set; }
        [Column("Apellidos")]
        public String Apellidos { get; set; }
        [Column("Titulo")]
        public String Titulo { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("Portada")]
        public String Portada { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
        [Column("PRECIOFINAL")]
        public int PrecioFinal { get; set; }

        public VistaPedidos () {}

        public VistaPedidos (int idVistaPedidos, int idUsuario, 
            string nombre, string apellidos, string titulo, int precio, 
            string portada, DateTime fecha, int precioFinal) {
            IdVistaPedidos = idVistaPedidos;
            IdUsuario = idUsuario;
            Nombre = nombre;
            Apellidos = apellidos;
            Titulo = titulo;
            Precio = precio;
            Portada = portada;
            Fecha = fecha;
            PrecioFinal = precioFinal;
        }
    }
}
