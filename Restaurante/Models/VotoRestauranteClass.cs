using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class VotoRestauranteClass
    {
        DB.RestaurantesContext db = new DB.RestaurantesContext();
        private string sql;

        public void AgregarVoto(int rating,int idrestaurante)
        {
            DB.RestauranteVotaciones voto = new DB.RestauranteVotaciones();
            voto.RestauranteId = idrestaurante;
            voto.Rating = Convert.ToInt16(rating);
            db.RestauranteVotaciones.Add(voto);
            db.SaveChanges();
        }
        public VotosModel GetVotos(int idrestaurante)
        {
            var result = from voto in db.RestauranteVotaciones
                         where voto.RestauranteId == idrestaurante
                         select new VotosModel
                         {
                             RestauranteId = Convert.ToInt32(voto.RestauranteId),
                             CantidadVoto = db.RestauranteVotaciones.Count(x=>x.RestauranteId == voto.RestauranteId),
                             Nota = Convert.ToInt32(db.RestauranteVotaciones.Where(x => x.RestauranteId == voto.RestauranteId).Sum(x => x.Rating) / db.RestauranteVotaciones.Count(x => x.RestauranteId == voto.RestauranteId))
                         };
            return result.First(); ;
        }
    }
}
