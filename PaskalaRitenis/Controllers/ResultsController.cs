using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        private bool ResultFileInUse(int id)
        {
            if (id > 0)
            {
                var year = _repository.GetYears().Where(x => x.RezultatsID == id).FirstOrDefault();
                if (year != null) return true;
            }
            return false;
        }

        public ActionResult GetFile(int id)
        {
            if (ResultFileInUse(id))
            {
                var file = _repository.GetResultById(id);
                string path;
                if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
                {
                    string webRootPath = Server.MapPath("~");
                    path = Path.GetFullPath(Path.Combine(webRootPath, "../Files/" + file.RezultatiLink));
                }
                else
                {
                    path = Path.Combine(ConfigurationManager.AppSettings["CustomUploadedFilesLocation"].Trim(), file.RezultatiLink);
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                string ext = Path.GetExtension(path);

                if(ext == ".pdf")
                {
                    return new FileContentResult(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf);
                }
                else
                {
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.RezultatiLink);
                }
            }
            return null;
        }

    }
}