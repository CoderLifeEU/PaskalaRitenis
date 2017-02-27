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
    public class ArchiveController : Controller
    {
        private IRezultatiRepository _repository;

        public ArchiveController()
            : this(new RezultatiRepository())
        {
        }

        public ArchiveController(IRezultatiRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var model = _repository.GetArchive().OrderByDescending(x => x.Year);
            return View(model);
        }

        private bool FileInUse(int id)
        {
            if (id > 0)
            {
                if (_repository.GetArchiveFileById(id) != null) return true;
            }
            return false;
        }

        public ActionResult GetFile(int id)
        {
            if (FileInUse(id))
            {
                var file = _repository.GetArchiveFileById(id);
                string path;
                if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
                {
                    string webRootPath = Server.MapPath("~/Files");
                    path = Path.GetFullPath(Path.Combine(webRootPath, file.FileName));
                }
                else
                {
                    path = Path.Combine(ConfigurationManager.AppSettings["CustomUploadedFilesLocation"].Trim(), file.FileName);
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                string ext = Path.GetExtension(path);

                if (ext == ".pdf")
                {
                    return new FileContentResult(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf);
                }
                else
                {
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
                }
            }
            return null;
        }

        public ActionResult Create()
        {
            List<RezultatiModel> years =_repository.GetYears().Distinct().ToList();
            TaskViewModel model = new TaskViewModel(years);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TaskViewModel model)
        {
            if(ModelState.IsValid)
            {
                string filename = _repository.InsertTask(model);
                string path;
                if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
                {
                    string webRootPath = Server.MapPath("~");
                    path = Path.GetFullPath(Path.Combine(webRootPath, "../PaskalaRitenis/Files/" + filename));
                }
                else
                {
                    path = Path.Combine(ConfigurationManager.AppSettings["CustomUploadedFilesLocation"].Trim(), Path.GetFileName(filename));
                }

                model.TaskFile.SaveAs(path);
                return RedirectToAction("Index");
            }
            List<RezultatiModel> years = _repository.GetYears().Distinct().ToList();
            model.SetYears(years);
            return View(model);
        }

    }
}