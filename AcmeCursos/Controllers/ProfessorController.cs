using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcmeCursos.ProfessorServices;
using AcmeCursos.Models;
using System.Net;
using AcmeCursos.ViewModel;

namespace AcmeCursos.Controllers
{
    public class ProfessorController : Controller
    {
        ProfessorServiceClient client = new ProfessorServiceClient();
        CursoServices.CursoServiceClient cursoClient = new CursoServices.CursoServiceClient();

        // GET: Professor
        public ActionResult Index()
        {
            List<Professor> professores = new List<Professor>(client.GetAll());
            return View(professores);
        }

        // GET: Professor/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           
            Professor professor = client.GetProfessorComCursos(id ?? 0);

            if (professor == null)
                return HttpNotFound();

            return View(professor);
        }

        // GET: Professor/Create
        public ActionResult Create()
        {
            ProfessorViewModel professorViewModel = new ProfessorViewModel()
            {
                Professor = new Professor() {
                    Cursos = new List<Curso>()
                }
            };

            var allCursos = cursoClient.GetAll();
            professorViewModel.TodosCursos = allCursos.Select(curso => new SelectListItem
            {
                Text = curso.Nome,
                Value = curso.Id.ToString()
            });

            return View(professorViewModel);
        }

        // POST: Professor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfessorViewModel professorViewModel)
        {
            if(ModelState.IsValid)
            {
                client.SaveWithCursos(professorViewModel.Professor,professorViewModel.CursosSelecionados.ToArray());
                return RedirectToAction("Index");
            }
            return View(professorViewModel);
        }

        // GET: Professor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ProfessorViewModel professorViewModel = new ProfessorViewModel()
            {
                Professor = client.Get(id ?? 0)
            };

            if (professorViewModel.Professor == null)
                return HttpNotFound();

            var allCursos = cursoClient.GetAll();
            professorViewModel.TodosCursos = allCursos.Select(curso => new SelectListItem
            {
                Text = curso.Nome,
                Value = curso.Id.ToString()
            });

            return View(professorViewModel);
        }

        // POST: Professor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProfessorViewModel professorViewModel)
        {
            if (ModelState.IsValid)
            {
                professorViewModel.Professor.Id = id;
                client.SaveWithCursos(professorViewModel.Professor, professorViewModel.CursosSelecionados.ToArray());
                return RedirectToAction("Index");
            }
            return View(professorViewModel);
        }

        // GET: Professor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Professor professor = client.Get(id ?? 0);

            if (professor == null)
                return HttpNotFound();

            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Professor professor)
        {
            try
            {
                client.Delete(professor.Id);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View(professor);
            }
        }
    }
}
