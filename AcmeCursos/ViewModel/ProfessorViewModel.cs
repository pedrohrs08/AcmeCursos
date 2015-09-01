using AcmeCursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcmeCursos.ViewModel
{
    public class ProfessorViewModel
    {
        public Professor Professor { get; set; }
        public IEnumerable<SelectListItem> TodosCursos {get; set;}

        private List<int> _cursosSelecionados;
        public List<int> CursosSelecionados
        {
            get
            {
               if(_cursosSelecionados == null && Professor.Cursos != null)
                    _cursosSelecionados = Professor.Cursos.Select(m => m.Id).ToList();
                return _cursosSelecionados;
            }
            set { _cursosSelecionados = value;  }
        }


    }
}