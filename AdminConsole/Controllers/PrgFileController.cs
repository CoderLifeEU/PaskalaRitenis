using AdminConsole.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConsole.Controllers
{
    public class PrgFileController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ProgFile prgFile)
        { 
            if(prgFile.File.ContentLength > 0)
            {
                var fileName = Path.GetFileName(prgFile.File.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                prgFile.File.SaveAs(path);
            }

            return RedirectToAction("Index");
        }
    }
}
