using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria_002.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_002.Controllers
{
    public class InquilinosController : Controller
    {
		private readonly IRepositorio<Inquilino> repositorio;

		public InquilinosController(IRepositorio<Inquilino> repositorio)
		{
			this.repositorio = repositorio;
		}

        // GET: Inquilino
        public ActionResult Index()
        {
		 var lista = repositorio.ObtenerTodos();
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            return View(lista);

        }

        // GET: Inquilino/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inquilino/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilino/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {
                TempData["Nombre"] = inquilino.Nombre;
				//if (ModelState.IsValid)
				//{
					repositorio.Alta(inquilino);
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

        [HttpPost]
        public JsonResult Buscar(string s)
        {
            var res = repositorio.ObtenerTodos().Where(x => x.Nombre.Contains(s) || x.Apellido.Contains(s));
            return new JsonResult(res);
        }

        // GET: Inquilino/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var inquilino = repositorio.ObtenerPorId(id);
            if (inquilino == null)
            {
                return NotFound();
            }
            return View(inquilino);
        }

        // POST: Inquilino/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, Inquilino inquilino)
        {
            //var miPropietario = repositorio.ObtenerPorId(id);

            if (id != inquilino.InquilinoId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    repositorio.Modificacion(inquilino);

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

        // GET: Inquilino/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inquilino/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var inquilino = repositorio.ObtenerPorId(id);

                repositorio.Baja(inquilino.InquilinoId);


                return RedirectToAction(nameof(Index));

                  
            }
            catch
            {
                return View();
            }
        }


        //// POST: Inquilinoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var inquilino = repositorio.ObtenerPorId(id);

        //    repositorio.Baja(inquilino.InquilinoId);
             
           
        //    return RedirectToAction(nameof(Index));
        //}

    }
}