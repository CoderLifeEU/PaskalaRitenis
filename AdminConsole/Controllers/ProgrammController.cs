using Repository.Models.OProgramma;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConsole.Controllers
{
    public class ProgrammController : Controller
    {
        private IOProgrammRepository _repository;

        public ProgrammController()
            : this(new OProgrammRepository())
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

        public ActionResult Create()
        {
            return View(new OProgrammModel());
        }

        [HttpPost]
        public ActionResult Create(OProgrammModel programm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    programm.Programma = System.Net.WebUtility.HtmlDecode(programm.Programma);
                    _repository.InsertProgramm(programm);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(programm);
        }

        public ActionResult Edit(int id)
        {
            OProgrammModel model = _repository.GetProgrammById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(OProgrammModel programm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    programm.Programma = System.Net.WebUtility.HtmlDecode(programm.Programma);
                    _repository.UpdateProgramm(programm);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(programm);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            OProgrammModel programm = _repository.GetProgrammById(id);
            return View(programm);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                OProgrammModel user = _repository.GetProgrammById(id);
                _repository.DeleteProgramm(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                new System.Web.Routing.RouteValueDictionary { 
          { "id", id }, 
          { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }

    }
}
