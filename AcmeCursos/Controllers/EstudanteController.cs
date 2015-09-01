using AcmeCursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AcmeCursos.Controllers
{
    public class EstudanteController : Controller
    {
        private EstudanteServices.EstudanteServiceClient client = new EstudanteServices.EstudanteServiceClient();
        // GET: Estudante
        public ActionResult Index()
        { 
            List<Estudante> estudantes = new List<Estudante>(client.GetAll()); 
            return View(estudantes);
        }

        // GET: Estudante/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estudante estudante = client.GetEstudanteComCursos(id ?? 0);
      
            if (estudante == null) {
                return HttpNotFound();
            }

            return View(estudante);
        }

        // GET: Estudante/Create
        public ActionResult Create()
        {
            return View(new Estudante() { DataMatricula = DateTime.Now });
        }

        // POST: Estudante/Create
        [HttpPost]
        public ActionResult Create(Estudante estudante)
        {
            if(ModelState.IsValid)
            {
                client.Save(estudante);
                return RedirectToAction("Index");
            }
            else
            {
                return View(estudante);
            }
        }

        // GET: Estudante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estudante estudante = client.Get(id ?? 0);

            if (estudante == null)
            {
                return HttpNotFound();
            }

            return View(estudante);
        }

        // POST: Estudante/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Estudante estudante)
        {
            if (ModelState.IsValid)
            {
                client.Save(estudante);
                return RedirectToAction("Index");
            }
            else
            {
                return View(estudante);
            }
        }

        // GET: Estudante/Delete/5
        public ActionResult Delete(int id)
        {
            return View(client.Get(id));
        }

        // POST: Estudante/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Estudante estudante)
        {
            try
            {
                client.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(estudante);
            }
        }
    }
}
