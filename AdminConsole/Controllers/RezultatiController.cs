using System.Data;
using System.Web.Mvc;
using AdminConsole.Models;
using System.IO;
using System.Web;
using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;

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
            {
                try
                {
                    string path;
                    if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
                    {
                        string webRootPath = Server.MapPath("~");
                        path = Path.GetFullPath(Path.Combine(webRootPath, "../Files/" + file.FileName));
                    }
                    else
                    {
                        path = Path.Combine(ConfigurationManager.AppSettings["CustomUploadedFilesLocation"].Trim(), Path.GetFileName(file.FileName));
                    }

                    file.SaveAs(path);
                    ViewBag.Message = "Fails veiksmīgi augšupielādēts";
                    ViewBag.fUpl = "true";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Kļūda: " + ex.Message.ToString();
                    ViewBag.fUpl = "true";
                }
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
            if (validateYear(year)) return _repository.InsertYear(new RezultatiModel()
            {
                Gads = int.Parse(year),
                RezultatiLink = link,
                Publicets = bool.Parse(publicet)
            });
            else return "Gads nav derīgs";
        }

        [HttpPost]
        public string InsertEmptyYear(string year, string publicet)
        {
            if (validateYear(year)) return _repository.InsertYear(new RezultatiModel()
            {
                Gads = int.Parse(year),
                Publicets = bool.Parse(publicet)
            });
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

        [HttpPost]
        public string UpdateYearLink(int ID, string link)
        {
            return _repository.UpdateYearLink(new RezultatiModel() { RezultatsID = ID, RezultatiLink = link });
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
            string path;
            if (ConfigurationManager.AppSettings["UseDefaultUploadPath"].Trim() == "true")
            {
                string webRootPath = Server.MapPath("~");
                path = Path.GetFullPath(Path.Combine(webRootPath, "../Files/"));
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

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string DeletePdf(string name)
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

            if (name.Trim() == String.Empty) return "Tukšs nosaukums!";
            else if (PdfInResultUse(name) != 0) return "Šis fails tiek izmantots " + PdfInResultUse(name) + ". gada rezultātu atspoguļošanai!";
            else if (PdfInArchiveUse(name) != 0) return "Šis fails ir piesaistīts " + PdfInArchiveUse(name) + ". gada arhīvā esošajiem uzdevumiem!";
            else if (!System.IO.File.Exists(path)) return name + " neeksistē!";
            else
            {
                try
                {
                    System.IO.File.Delete(path);
                    return name + " veiksmīgi dzēsts!";
                }
                catch (Exception ex)
                {
                    return "Kļūda: " + ex.Message.ToString();
                }
            }
        }

        private int PdfInResultUse(string pdfName)
        {
            var years = _repository.GetYears();
            foreach (var year in years)
            {
                if (year.RezultatiLink == pdfName) return year.Gads;
            }
            return 0;
        }

        private int PdfInArchiveUse(string pdfName)
        {
            ArchiveRepository ar = new ArchiveRepository();
            var years = ar.GetYears();
            foreach (var year in years)
            {
                if (year.FileName == pdfName) return year.Year;
            }
            return 0;
        }

        public FileResult GetFile(string name)
        {
            if (name != null && name.Length > 0 && PdfInResultUse(name) != 0)
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

                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
            }
            return null;
        }

        public ActionResult GetPdf(string name)
        {
            if (name != null && name.Length > 0 && PdfInResultUse(name) != 0)
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

                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                string ext = Path.GetExtension(path);

                if (ext == ".pdf")
                {
                    return new FileContentResult(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf);
                }
                else
                {
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
                }
            }
            return null;
        }

    }
}