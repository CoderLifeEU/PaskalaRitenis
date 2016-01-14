using System.Data;
using System.Web.Mvc;
using AdminConsole.Models;
using System.IO;
using System.Web;
using System;
using System.Collections.Generic;
using System.Net;

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

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
            {
                    string path = Path.Combine(Server.MapPath("~/RezultatiPdf"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "Fails veiksmīgi augšupielādēts";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Kļūda:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Izvēlieties failu (pdf):";
            }
            return View();
        }

        public JsonResult GetYears()
        {   
            var result = _repository.GetYears();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string InsertYear(string year, string link, string publicet)
        {
            if (validateYear(year)) return _repository.InsertYear(new RezultatiModel() { Gads = int.Parse(year), RezultatiLink = link, Publicets = bool.Parse(publicet) });
            else return "Gads nav derīgs";
        }

        [HttpPost]
        public string DeleteYear(string id)
        {
            return _repository.DeleteYear(int.Parse(id));
        }

        [HttpPost]
        public string UpdateYear(int ID, bool publicet, bool arhivet)
        {
            return _repository.UpdateYear(new RezultatiModel() { RezultatsID = ID, Publicets = publicet, Arhivets = arhivet });
        }

        private bool validateYear(string year)
        {
            try
            {
                int yearParsed = int.Parse(year);
                if (yearParsed >= 2006 && yearParsed < 10000) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public JsonResult GetFiles()
        {
            var path = HttpContext.Server.MapPath("~/RezultatiPdf");
            var faili = Directory.GetFiles(path);
            List<FileModel> result = new List<FileModel>();
            foreach (var fails in faili)
            {
                var name = Path.GetFileName(fails);
                result.Add(new FileModel() { Name = name, Path = fails });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
