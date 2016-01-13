using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaskalaRitenis.Models;
using PaskalaRitenis.Helpers;

namespace PaskalaRitenis.Controllers
{
    public class SupportersController : Controller
    {

        public ActionResult Index()
        {
            var supporters = SerializeUtils.DeserializeFromXML<SupportersModel>(Server.MapPath(Url.Content("~/Content/Supporters.xml"))).Supporters;

            return View(supporters);
        }

    }
}
