using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Repository.Models.OProgramma;

namespace PaskalaRitenis.Controllers
{
    public class ProgrammController : Controller
    {
        private IOProgrammRepository _repository;

        public ProgrammController() :this(new OProgrammRepository())
        { 
        }

        public ProgrammController(IOProgrammRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var programm = _repository.GetProgramm();
            return View(programm);
        }

    }
}
