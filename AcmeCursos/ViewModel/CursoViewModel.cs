using AcmeCursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcmeCursos.ViewModel
{
    public class CursoViewModel
    {
        public Curso Curso { get; set; }
        public IEnumerable<SelectListItem> TodosProfessores {get; set;}

        private List<int> _professoresSelecionados;
        public List<int> ProfessoresSelecionados
        {
            get
            {
               if(_professoresSelecionados == null && Curso.Professores != null)
                    _professoresSelecionados = Curso.Professores.Select(m => m.Id).ToList();
                return _professoresSelecionados;
            }
            set { _professoresSelecionados = value;  }
        }


    }
}