using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria_002.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_002.Controllers
{
    public class InmueblesController : Controller
    { 
        private readonly IRepositorioInmueble repositorio;
        private readonly IRepositorioPropietario repoPropietario;

        public InmueblesController(IRepositorioInmueble repositorio, IRepositorioPropietario repoPropietario)
		{
			this.repositorio = repositorio;
            this.repoPropietario = repoPropietario;
        }

        public ActionResult Index()
        {
		 var lista = repositorio.ObtenerTodos();
          
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];

            return View(lista);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        // Inquilino/Create
        public ActionResult Create()
        {
            ViewBag.Propietarios = repoPropietario.ObtenerTodos();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                    
                TempData["Nombre"] = inmueble.Direccion;
                repositorio.Alta(inmueble);
                return RedirectToAction(nameof(Index));
               
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Buscar(string s)
        {
            var res = repositorio.ObtenerTodos().Where(x => x.Direccion.Contains(s));
            return new JsonResult(res);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            ViewBag.Propietarios = repoPropietario.ObtenerTodos();
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, Inmueble inmueble)
        {
            if (id != inmueble.InmuebleId)
            {
                return NotFound();
            }
                try
                {
                    repositorio.Modificacion(inmueble);

                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    ViewBag.StackTrate = ex.StackTrace;
                    return View();
                }
                return RedirectToAction(nameof(Index));
         
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var inmueble = repositorio.ObtenerPorId(id);
                repositorio.Baja(inmueble.InmuebleId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}