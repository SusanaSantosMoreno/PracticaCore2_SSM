using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2_SSM.Models {
    [Table("USUARIOS")]
    public class Usuario {

        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("Nombre")]
        public String Nombre { get; set; }
        [Column("Apellidos")]
        public String Apellidos { get; set; }
        [Column("Email")]
        public String Email { get; set; }
        [Column("Pass")]
        public String Password { get; set; }
        [Column("Foto")]
        public String Foto { get; set; }

        public Usuario () {}

        public Usuario (int idUsuario, string nombre, string apellidos, 
            string email, string password, string foto) {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Apellidos = apellidos;
            Email = email;
            Password = password;
            Foto = foto;
        }
    }
}
