using PracticaCore2_SSM.Data;
using PracticaCore2_SSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region VISTAS
/*
  CREATE VIEW LibrosPag
AS
	SELECT ROW_NUMBER() OVER (ORDER BY idLibro)
	AS POSICION
	, libros.* FROM libros
GO
 */
#endregion
namespace PracticaCore2_SSM.Repositories {

    public class LibrosRepository {

        LibrosContext context;

        public LibrosRepository (LibrosContext context) { this.context = context; }

        #region GENEROS

        public List<Genero> GetGeneros () {
            return this.context.Generos.ToList();
        }

        public Genero GetGenero (int idGenero) {
            return this.context.Generos.FirstOrDefault(x => x.IdGenero == idGenero);
        }

        #endregion
        #region LIBROS

        public List<Libro> GetLibros () {
            return this.context.Libros.ToList();
        }

        public List<LibroPag> GetLibrosPaginados(int posicion, ref int registros) {
            registros = this.context.LibrosPag.Count();
            return this.context.LibrosPag.
                Where(x => x.Posicion >= posicion && x.Posicion < posicion + 6).ToList();
        }

        public List<LibroPag> GetLibrosPaginados () {
            return this.context.LibrosPag.ToList();
        }

        public List<LibroPag> GetLibrosPaginados (int idGenero) {
            return this.context.LibrosPag.Where(x => x.IdGenero == idGenero).ToList();
        }

        public List<Libro> GetLibros (int idGenero) {
            return this.context.Libros.Where(x=> x.IdGenero==idGenero).ToList();
        }

        public Libro GetLibro (int idLibro) {
            return this.context.Libros.FirstOrDefault(x=> x.IdLibro == idLibro);
        }

        #endregion
        #region PEDIDOS

        public void guardarPedido (int idLibro,int idPedido, int idFactura, int cantidad, int idUsuario) {
            Pedido pedido = new Pedido(idPedido, idFactura, DateTime.Now, idLibro, idUsuario, cantidad);
            this.context.Pedidos.Add(pedido);
            this.context.SaveChanges();
        }

        public int getUltimaFactura () {
            int factura = this.context.Pedidos.OrderByDescending(x => x.IdPedido).FirstOrDefault().IdFactura;
            return factura;
        }


        public Pedido getUltimoPedido () {
            Pedido lastPedido = this.context.Pedidos.OrderByDescending(x => x.IdPedido).FirstOrDefault();
            return lastPedido;
        }
        #endregion
        #region VISTAPEDIDOS

        public List<VistaPedidos> GetPedidosUsuario(int idUsuario) {
            return this.context.VistaPedidos.Where(x => x.IdUsuario == idUsuario).ToList();
        }

        #endregion
        #region USUARIOS

        public Usuario GetUsuario(String email, String password) {
            return this.context.Usuarios.
                FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public Usuario GetUsuario (int idUsuario) {
            return this.context.Usuarios.
                FirstOrDefault(x => x.IdUsuario == idUsuario);
        }
        #endregion
    }
}
