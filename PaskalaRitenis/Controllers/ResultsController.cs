using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Controllers
{
    public class ResultsController : Controller
    {
        private IRezultatiRepository _repository;

        public ResultsController()
            : this(new RezultatiRepository())
        {
        }

        public ResultsController(IRezultatiRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var model = _repository.GetYears().Where(x => x.Publicets).OrderByDescending(x => x.Gads);
            return View(model);
        }

        private int ResultFileInUse(string pdfName)
        {
            var years = _repository.GetYears();
            foreach (var year in years)
            {
                if (year.RezultatiLink == pdfName) return year.Gads;
            }
            return 0;
        }

        public FileResult GetFile(string name)
        {
            if (name != null && name.Length > 0 && ResultFileInUse(name) != 0)
            {
                string path;
                if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
                {
                    string webRootPath = Server.MapPath("~");
                    path = Path.GetFullPath(Path.Combine(webRootPath, "../AdminConsole/Uploads/" + name));
                }
                else
                {
                    path = Path.Combine(ConfigurationManager.AppSettings["CustomUploadedFilesLocation"].Trim(), name);
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
            }
            return null;
        }

        public ActionResult GetPdf(string name)
        {
            if (name != null && name.Length > 0 && ResultFileInUse(name) != 0)
            {
                string path;
                if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
                {
                    string webRootPath = Server.MapPath("~");
                    path = Path.GetFullPath(Path.Combine(webRootPath, "../AdminConsole/Uploads/" + name));
                }
                else
                {
                    path = Path.Combine(ConfigurationManager.AppSettings["CustomUploadedFilesLocation"].Trim(), name);
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return new FileContentResult(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf);
            }
            return null;
        }

    }
}