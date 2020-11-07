using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class UsuarioClass
    {
        DB.RestaurantesContext db = new DB.RestaurantesContext();
        public DB.Usuario ValidarUsuario(DB.Usuario usuario)
        {
            var result = (from user in db.Usuario where user.Correo == usuario.Correo && user.Password == usuario.Password select user);
            return result.First();
        }
    }
}
