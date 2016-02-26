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
            var supporters = GetSupporters();

            return View(supporters);
        }

        public PartialViewResult GetSupportersList()
        {
            var supporters = GetSupporters();

            IEnumerable<Supporter> supportersRandom = Helpers.Utils.Randomize<Supporter>(supporters);

            return PartialView("~/Views/Supporters/SupportersList.cshtml", supportersRandom);
        }

        private List<Supporter> GetSupporters()
        {
            return SerializeUtils.DeserializeFromXML<SupportersModel>(Server.MapPath(Url.Content("~/Content/Supporters.xml"))).Supporters;
        }

    }
}
