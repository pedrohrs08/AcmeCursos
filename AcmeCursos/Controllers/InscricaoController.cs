using AcmeCursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcmeCursos.Controllers
{
    public class InscricaoController : Controller
    {
        private EstudanteServices.EstudanteServiceClient estudanteClient = new EstudanteServices.EstudanteServiceClient();
        private CursoServices.CursoServiceClient cursosClient = new CursoServices.CursoServiceClient();
        private InscricaoServices.InscricaoServiceClient inscricaoClient = new InscricaoServices.InscricaoServiceClient();

        public ActionResult InscreverAlunoEmCurso()
        {
            populateSelectLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InscreverAlunoEmCurso(Inscricao inscricao) {

            if (ModelState.IsValid) {
                inscricao.DataInscricao = DateTime.Now;
                inscricaoClient.Save(inscricao);
                return View("Sucesso");
            }
            return View(inscricao);
        }

        private void populateSelectLists() {
            var estudanteOptions = estudanteClient.GetAll().Select(e => new SelectListItem()
            {
                Text = string.Format("{0} {1}", e.Nome, e.Sobrenome),
                Value = e.Id.ToString()
            });

            var cursoOptions = cursosClient.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            });

            ViewBag.CursoId = cursoOptions;
            ViewBag.EstudanteId = estudanteOptions;
        }
    }
}