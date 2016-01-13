using System.Data;
using System.Web.Mvc;
using AdminConsole.Models;

namespace AdminConsole.Controllers
{
    public class RezultatiController : Controller
    {
        private IRezultatiRepository _repository;

        public RezultatiController()
            : this(new RezultatiRepository())
        {
        }

        public RezultatiController(IRezultatiRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult Details(string _year)
        //{
        //    if (validateYear(_year))
        //    {
        //        var result = _repository.GetFormasByYear(int.Parse(_year));
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        var result = new JsonResult();
        //        result.Data = "Gads nav derīgs";
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult GetYears()
        {   
            var result = _repository.GetYears();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetForms()
        {
            var result = _repository.GetFormas();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAttachedForma(string _year)
        {
            if (validateYear(_year))
            {
                var result = _repository.GetAttachedForma(int.Parse(_year));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new JsonResult();
                result.Data = "Gads nav derīgs";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public string InsertNewYear(string _year)
        {
            if (validateYear(_year))
            {
                _repository.InsertYear(int.Parse(_year));
                return "Saglabāts";
            }
            else return "Gads nav derīgs";
        }

        [HttpPost]
        public string InsertNewForm(string title)
        {
            try
            {
                MvcHtmlString encoded = new MvcHtmlString(title);
                _repository.InsertForm(new FormaModel() { FormaNosaukums = encoded.ToString(), FormaXML = "<tests/>" });
                return "Saglabāts";
            }
            catch
            {
                return "Kļūda";
            }
        }

        [HttpPost]
        public string DeleteYear(string _year)
        {
            if (validateYear(_year)) {
                _repository.DeleteYear(int.Parse(_year));
                return "Nodzēsts";
            }
            else return "Gads nav derīgs";
        }

        [HttpPost]
        public string BindForm(string formId, string year)
        {
            if (validateFormID(formId) && validateYear(year))
            {
                _repository.BindForm(int.Parse(formId), int.Parse(year));
                return "Savienots";
            }
            else return "Parametri nav derīgi";
        }

        [HttpPost]
        public string DeleteForm(string formID)
        {
            if (validateFormID(formID)) {
                _repository.DeleteForm(int.Parse(formID));
                return "Nodzēsts";
            }
            else return "Formas ID nav derīgs";
        }

        private bool validateYear(string _year)
        {
            try
            {
                int year = int.Parse(_year);
                if (year >= 2006 && year < 10000) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        private bool validateFormID(string _formID)
        {
            try
            {
                int id = int.Parse(_formID);
                if (id > 0) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
