using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcmeCursos.CursoServices;
using AcmeCursos.Models;
using System.Net;
using AcmeCursos.ViewModel;

namespace AcmeCursos.Controllers
{
    public class CursoController : Controller
    {
        CursoServiceClient client = new CursoServiceClient();
        ProfessorServices.ProfessorServiceClient professorClient = new ProfessorServices.ProfessorServiceClient();
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

            Curso curso = client.GetCursoComProfessores(id ?? 0);

            if (curso == null)
                return HttpNotFound();

            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            CursoViewModel cursoViewModel = new CursoViewModel()
            {
                Curso = new Curso()
                {
                   Professores = new List<Professor>()
                }
            };

            var allProfessores = professorClient.GetAll();
            cursoViewModel.TodosProfessores = allProfessores.Select(professor => new SelectListItem
            {
                Text = professor.Nome,
                Value = professor.Id.ToString()
            });
            return View(cursoViewModel);
        }

        // POST: Curso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CursoViewModel cursoViewModel)
        {
            if(ModelState.IsValid)
            {
                client.SaveWithProfessores(cursoViewModel.Curso,cursoViewModel.ProfessoresSelecionados.ToArray());
                return RedirectToAction("Index");
            }
            else
            {
                return View(cursoViewModel);
            }
        }

        // GET: Curso/Edit/5
        public ActionResult Edit(int? id)
        {
            if(!id.HasValue)
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CursoViewModel cursoViewModel = new CursoViewModel()
            {
                Curso = client.Get(id ?? 0)
            };

            if (cursoViewModel.Curso == null)
                return HttpNotFound();

            var allProfessores = professorClient.GetAll();
            cursoViewModel.TodosProfessores = allProfessores.Select(professor => new SelectListItem
            {
                Text = professor.Nome,
                Value = professor.Id.ToString()
            });

            return View(cursoViewModel);
        }

        // POST: Curso/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CursoViewModel cursoViewModel)
        {
            if (ModelState.IsValid) { 
                client.SaveWithProfessores(cursoViewModel.Curso,cursoViewModel.ProfessoresSelecionados.ToArray());

                return RedirectToAction("Index");
            }
            else
            {
                return View(cursoViewModel);
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
