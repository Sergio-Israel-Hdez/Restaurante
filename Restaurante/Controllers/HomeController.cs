using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurante.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace Restaurante.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UsuarioClass UsuarioClass = new UsuarioClass();
        public RestauranteClass restauranteClass = new RestauranteClass();
        const string SessionRol = "_Rol";
        const int pageSize = 5;
        
        public IActionResult Index(int? page,string orden)
        {
            var result = restauranteClass.GetAllRestaurante();
            ViewBag.ordenVotos = String.IsNullOrEmpty(orden) ? "voto_desc" : "";
            ViewBag.ordenid = orden == "id" ? "id_desc" : "id";

            int pageNumber = (page ?? 1);

            switch (orden)
            {
                case "voto_desc":
                    result = result.OrderByDescending(x => x.Votos);
                    break;
                case "id":
                    result = result.OrderBy(x => x.RestauranteId);
                    break; 
                case "id_desc":
                    result = result.OrderByDescending(x => x.RestauranteId);
                    break;
                default:
                    result = result.OrderBy(x => x.Votos);
                    break;
            }
            return View(result.ToList().ToPagedList(pageNumber,pageSize));
        }
        
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Models.DB.Usuario _usuario)
        {
            if (_usuario.Correo == null || _usuario.Password==null)
            {
                return View(_usuario);
            }
            var result = UsuarioClass.ValidarUsuario(_usuario);
            if (result!=null)
            {
                HttpContext.Session.SetInt32(SessionRol,result.Rol);
                return RedirectToAction("Index", "Administrador");
            }
            return View(_usuario);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
