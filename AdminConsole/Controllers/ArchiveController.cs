using AdminConsole.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConsole.Controllers
{
    public class ArchiveController : Controller
    {
        private IArchiveRepository _repository;

        public ArchiveController()
            : this(new ArchiveRepository())
        {
        }

        public ArchiveController(IArchiveRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(string year)
        {
            if (!String.IsNullOrEmpty(year)) ViewBag.Year = year;
            else ViewBag.Year = 0;
            return View();
        }

        public JsonResult GetFiles(string year)
        {
            if (!String.IsNullOrEmpty(year))
            {
                var result = _repository.GetYearsByYearValue(Int32.Parse(year));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        /// <summary>
        /// Gets all the files except for the ones selected for the year specified
        /// </summary>
        public JsonResult GetAllFiles(string year)
        {
            if (!String.IsNullOrEmpty(year))
            {
                List<FileModel> uploaded = GetFilesFromUpload();
                IEnumerable<ArchiveModel> years = _repository.GetYearsByYearValue(Int32.Parse(year));
                if (years != null && years.Count() > 0)
                {
                    foreach (var x in years)
                    {
                        if (uploaded.Where(z => z.Name == x.FileName).Count() > 0)
                        {
                            uploaded.RemoveAll(c => c.Name == x.FileName);
                        }
                    };
                }
                return Json(uploaded, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public List<FileModel> GetFilesFromUpload()
        {
            string path;
            if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
            {

                string webRootPath = Server.MapPath("~");
                path = Path.GetFullPath(Path.Combine(webRootPath, "../PaskalaRitenis/Uploads/"));
            }
            else
            {
                path = ConfigurationManager.AppSettings["CustomUploadedFilesLocation"].Trim();
            }

            var faili = Directory.GetFiles(path);
            List<FileModel> result = new List<FileModel>();
            foreach (var fails in faili)
            {
                var name = Path.GetFileName(fails);
                result.Add(new FileModel() { Name = name });
            }

            return result;
        }

        [HttpPost]
        public string DeleteFile(string id)
        {
            if (id != null && Int32.Parse(id.Trim()) > 0)
            {
                return _repository.DeleteYearById(int.Parse(id.Trim()));
            }
            else return "Kļūdains faila Id!";
        }

        [HttpPost]
        public string AttachFile(string name, int year)
        {
            string path;
            if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
            {
                string webRootPath = Server.MapPath("~");
                path = Path.GetFullPath(Path.Combine(webRootPath, "../Files/" + name));
            }
            else
            {
                path = Path.Combine(ConfigurationManager.AppSettings["CustomUploadedFilesLocation"].Trim(), name);
            }
            if (year <= 0) return "Nederīgs gads!";
            else if (name.Trim() == String.Empty) return "Tukšs nosaukums!";
            else if (!System.IO.File.Exists(path)) return name + " neeksistē!";
            else
            {
                try
                {
                    return _repository.InsertFile(new ArchiveModel()
                    {
                        Year = year,
                        FileName = name
                    });
                }
                catch (Exception ex)
                {
                    return "Kļūda: " + ex.Message.ToString();
                }
            }
        }

    }
}