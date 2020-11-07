using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class ItemDestacadoClass
    {
        DB.RestaurantesContext db = new DB.RestaurantesContext();
        public IEnumerable<Models.DB.ItemDestacado> ListaItemRestaurante(int id)
        {
            var result = from res in db.Restaurante
                         join item in db.ItemDestacado
                         on res.RestauranteId equals item.RestauranteId
                         where res.RestauranteId == id
                         select item;
            return result;
        }
        public DB.ItemDestacado Detalle(int id)
        {
            var result = db.ItemDestacado.Find(id);
            return result;
        }
        public void AgregarItem(DB.ItemDestacado item,byte[] image)
        {
            DB.ItemDestacado new_item = new DB.ItemDestacado();
            new_item.Descripcion = item.Descripcion;
            new_item.Foto = image;
            new_item.RestauranteId = item.RestauranteId;
            db.ItemDestacado.Add(new_item);
            db.SaveChanges();
        }
        public void EliminarItem(int id)
        {
            var result = db.ItemDestacado.Find(id);
            db.ItemDestacado.Remove(result);
            db.SaveChanges();
        }
        public void ModificarItem(DB.ItemDestacado item,byte[] image)
        {
            DB.ItemDestacado old_item = db.ItemDestacado.Find(item.ItemDestacadoId);
            old_item.Descripcion = item.Descripcion;
            old_item.Foto = image;
            db.SaveChanges();
        }
    }
}
