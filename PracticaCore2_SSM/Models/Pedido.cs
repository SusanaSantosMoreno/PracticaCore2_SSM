using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Models {
    [Table("PEDIDOS")]
    public class Pedido {

        [Key]
        [Column("IdPedido")]
        public int IdPedido { get; set; }
        [Column("IdFactura")]
        public int IdFactura { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
        [Column("IdLibro")]
        public int IdLibro { get; set; }
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("Cantidad")]
        public int Cantidad { get; set; }

        public Pedido () { }

        public Pedido (int idPedido, int idFactura, DateTime fecha, 
            int idLibro, int idUsuario, int cantidad) {
            IdPedido = idPedido;
            IdFactura = idFactura;
            Fecha = fecha;
            IdLibro = idLibro;
            IdUsuario = idUsuario;
            Cantidad = cantidad;
        }
    }
}
