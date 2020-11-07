using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Restaurante.Models;
using System.IO;
using X.PagedList;

namespace Restaurante.Controllers
{
    public class AdministradorController : Controller
    {
        const string SessionRol = "_Rol";
        public RestauranteClass restauranteClass = new RestauranteClass();
        public ItemDestacadoClass itemDestacadoClass = new ItemDestacadoClass();
        const int pageSize = 5;
        public IActionResult Index(int? page,string orden)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            return View(result.ToList().ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Create()
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Models.DB.Restaurante in_restaurante, List<IFormFile> Logo)
        {
            if (!ModelState.IsValid)
            {
                return View(in_restaurante);
            }
            byte[] image = null;
            foreach (var item in Logo)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        image = stream.ToArray();
                    }
                }
            }
            restauranteClass.AgregarRestaurante(in_restaurante, image);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.itemDestacado = itemDestacadoClass.ListaItemRestaurante(Convert.ToInt32(id));
            var result = restauranteClass.Detalle(Convert.ToInt32(id));
            return View(result);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var result = restauranteClass.Detalle(Convert.ToInt32(id));
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Models.DB.Restaurante in_restaurante, List<IFormFile> Logo)
        {
            if (!ModelState.IsValid)
            {
                return View(in_restaurante);
            }
            byte[] image = null;
            foreach (var item in Logo)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        image = stream.ToArray();
                    }
                }
            }
            restauranteClass.Modificar(in_restaurante, image);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return RedirectToAction("Index");
            }
            restauranteClass.Eliminar(Convert.ToInt32(id));
            return RedirectToAction("Index");
        }
        public IActionResult Desconectar()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
