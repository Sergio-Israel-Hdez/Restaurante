using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Restaurante.Controllers
{
    public class VotoController : Controller
    {
        Models.VotoRestauranteClass votoRestaurante = new Models.VotoRestauranteClass();
        [HttpGet]
        public IActionResult Voto(int? rating,int? idrestaurante)
        {
            if (rating!=null && idrestaurante!=null)
            {
                votoRestaurante.AgregarVoto(Convert.ToInt32(rating), Convert.ToInt32(idrestaurante));
                return RedirectToAction("Index", "Administrador");
            }
            return RedirectToAction("Index", "Administrador");
        }
        [HttpGet]
        public Models.VotosModel VotoRestaurante(int? idrestaurante)
        {
            if (idrestaurante==null)
            {
                return null;
            }
            var result = votoRestaurante.GetVotos(Convert.ToInt32(idrestaurante));
            return result;
        }
    }
}
