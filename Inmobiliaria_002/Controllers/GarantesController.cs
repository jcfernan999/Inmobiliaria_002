using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria_002.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace Inmobiliaria_002.Controllers
{
    public class GarantesController : Controller

    {
        private readonly IRepositorio<Garante> repositorio;

        public GarantesController(IRepositorio<Garante> repositorio)
        {
            this.repositorio = repositorio;
        }


        // GET: Garantes
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerTodos();
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            return View(lista);
        }

        // GET: Garantes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Garantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Garantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Garante garante)
        {
            try
            {
                TempData["Nombre"] = garante.Nombre;
                //if (ModelState.IsValid)
                //{
                repositorio.Alta(garante);
                return RedirectToAction(nameof(Index));
                //}
                //else
                //	return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Garantes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garante = repositorio.ObtenerPorId(id);
            if (garante == null)
            {
                return NotFound();
            }
            return View(garante);
        }

        // POST: Inquilino/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, Garante garante)
        {
            //var miPropietario = repositorio.ObtenerPorId(id);

            if (id != garante.GaranteId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                repositorio.Modificacion(garante);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View();
            }
            return RedirectToAction(nameof(Index));
            //}
            //return View();
        }

        // GET: Garantes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Garantes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var garante = repositorio.ObtenerPorId(id);

                repositorio.Baja(garante.GaranteId);


                return RedirectToAction(nameof(Index));


            }
            catch
            {
                return View();
            }
        }
    }
}