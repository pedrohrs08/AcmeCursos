using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcmeCursos.CursoServices;
using AcmeCursos.Models;
using System.Net;

namespace AcmeCursos.Controllers
{
    public class CursoController : Controller
    {
        CursoServiceClient client = new CursoServiceClient();
        // GET: Curso
        public ActionResult Index()
        {
            List<Curso> curso = new List<Curso>(client.GetAll());
            return View(curso);
        }

        // GET: Curso/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Curso curso = client.Get(id ?? 0);

            if (curso == null)
                return HttpNotFound();

            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            return View(new Curso { DataLimiteInscricao = DateTime.Now });
        }

        // POST: Curso/Create
        [HttpPost]
        public ActionResult Create(Curso curso)
        {
            try
            {
                client.Save(curso);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(curso);
            }
        }

        // GET: Curso/Edit/5
        public ActionResult Edit(int? id)
        {
            if(!id.HasValue)
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Curso curso = client.Get(id ?? 0);

            if (curso == null)
                return HttpNotFound();

            return View(curso);
        }

        // POST: Curso/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Curso curso)
        {
            try
            {
                client.Save(curso);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Curso/Delete/5
        public ActionResult Delete(int? id)
        {
            if(!id.HasValue)
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Curso curso = client.Get(id ?? 0);

            if (curso == null)
                return HttpNotFound();

            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Curso curso)
        {
            try
            {
                client.Delete(curso.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
