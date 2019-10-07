using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria_002.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_002.Controllers
{
    public class AlquilerController : Controller
    {
        private readonly IRepositorio<Alquiler> repositorio;
        private readonly IRepositorioInmueble repoInmuebles;
        private readonly IRepositorioInquilino repoInquilinos;
        
        public AlquilerController(IRepositorioAlquiler repositorio, IRepositorioInmueble repoInmuebles, IRepositorioInquilino repoInquilinos)
     
        {
            this.repositorio = repositorio;
            this.repoInmuebles = repoInmuebles;
            this.repoInquilinos = repoInquilinos;
            
        }

        // GET: Alquiler
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerTodos();
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            return View(lista);
        }

        // GET: Alquiler/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Inmuebles = repoInmuebles.ObtenerTodos();
            return View();
        }

        // GET: Alquiler/Create
        public ActionResult Create()
        {

            ViewBag.Inmuebles = repoInmuebles.ObtenerTodos();
            ViewBag.Inquilinos = repoInquilinos.ObtenerTodos();
            
            return View();
        }

        // POST: Alquiler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alquiler alquiler)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorio.Alta(alquiler);
                    TempData["Id"] = alquiler.AlquilerId;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Alquileres = repositorio.ObtenerTodos();
                    return View(alquiler);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(alquiler);
            }
        }

        [HttpPost]
        public JsonResult Buscar(DateTime s)
        {
            var res = repositorio.ObtenerTodos().Where(x => x.FechaAlta == s);
            return new JsonResult(res);
        }

        // GET: Propietario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = repositorio.ObtenerPorId(id);
            if (alquiler == null)
            {
                return NotFound();
            }
            return View(alquiler);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Alquiler alquiler)
        {
            //var miPropietario = repositorio.ObtenerPorId(id);

            if (id != alquiler.AlquilerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repositorio.Modificacion(alquiler);
                     
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    ViewBag.StackTrate = ex.StackTrace;
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Propietario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Alquiler miAlquiler)
        {
            try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(miAlquiler);
            }
        }
    }
}