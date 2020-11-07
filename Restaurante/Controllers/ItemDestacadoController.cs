using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    public class ItemDestacadoController : Controller
    {
        const string SessionRol = "_Rol";
        ItemDestacadoClass itemDestacadoClass = new ItemDestacadoClass();
        [HttpGet]
        public IActionResult CreateItem(int? id)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id==null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.idRestaurante = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>CreateItem(Models.DB.ItemDestacado item_destacado, List<IFormFile> Foto)
        {
            if (!ModelState.IsValid)
            {
                return View(item_destacado);
            }
            byte[] image = null;
            foreach (var item in Foto)
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
            itemDestacadoClass.AgregarItem(item_destacado, image);
            return RedirectToAction("Details", "Administrador", new { id = item_destacado.RestauranteId });
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var result = itemDestacadoClass.Detalle(Convert.ToInt32(id));
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.DB.ItemDestacado item_restaurante, List<IFormFile> Foto)
        {
            if (!ModelState.IsValid)
            {
                return View(item_restaurante);
            }
            byte[] image = null;
            foreach (var item in Foto)
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
            itemDestacadoClass.ModificarItem(item_restaurante, image);
            return RedirectToAction("Details", "Administrador", new { id = item_restaurante.RestauranteId });

        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var result = itemDestacadoClass.Detalle(Convert.ToInt32(id));
            return View(result);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            itemDestacadoClass.EliminarItem(Convert.ToInt32(id));
            return RedirectToAction("Index","Administrador");
        }
    }
}
