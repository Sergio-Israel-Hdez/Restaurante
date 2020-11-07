using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class RestauranteClass
    {
        DB.RestaurantesContext db = new DB.RestaurantesContext();
        public IQueryable<DB.Restaurante> GetAllRestaurante()
        {
            var result = (from re in db.Restaurante
                          select new DB.Restaurante
                          {
                              NombreRestaurante = re.NombreRestaurante,
                              HoraApertura = re.HoraApertura,
                              HoraCierre = re.HoraCierre,
                              ItemDestacado = (from i in db.ItemDestacado where i.RestauranteId == re.RestauranteId select i).ToList(),
                              RestauranteVotaciones = (from v in db.RestauranteVotaciones where v.RestauranteId == re.RestauranteId select v).ToList(),
                              Logo = re.Logo,
                              RestauranteId = re.RestauranteId,
                              Votos = re.Votos
                          });
            return result;
        }
        public void AgregarRestaurante(DB.Restaurante _restaurante,byte[] image)
        {
            DB.Restaurante new_restaurante = new DB.Restaurante();
            new_restaurante.NombreRestaurante = _restaurante.NombreRestaurante;
            new_restaurante.HoraApertura = _restaurante.HoraApertura;
            new_restaurante.HoraCierre = _restaurante.HoraCierre;
            new_restaurante.Logo = image;
            db.Restaurante.Add(new_restaurante);
            db.SaveChanges();
        }
        public void Eliminar(int idrestaurante)
        {
            var result = db.Restaurante.Find(idrestaurante);
            db.Restaurante.Remove(result);
            db.SaveChanges();
        }
        public void Modificar(DB.Restaurante _restaurante,byte[] image)
        {
            DB.Restaurante old_restaurante = db.Restaurante.Find(_restaurante.RestauranteId);
            old_restaurante.NombreRestaurante = _restaurante.NombreRestaurante;
            old_restaurante.HoraApertura = _restaurante.HoraApertura;
            old_restaurante.HoraCierre = _restaurante.HoraCierre;
            old_restaurante.Logo = image;
            db.SaveChanges();
        }
        public DB.Restaurante Detalle(int id)
        {
            var result = db.Restaurante.Find(id);
            return result;
        }
    }
}
