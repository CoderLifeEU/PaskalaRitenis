using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Controllers
{
    public class HomeController : Controller
    {
        private INewsRepository _repository;
        public HomeController()
            : this(new NewsRepository())
        {
        }
        public HomeController(INewsRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var news = _repository.GetNews();

            var supporters = Helpers.SerializeUtils.DeserializeFromXML<SupportersModel>(Server.MapPath(Url.Content("~/Content/Supporters.xml"))).Supporters;

            IEnumerable<Supporter> supportersRandom = Helpers.Utils.Randomize<Supporter>(supporters);

            var model = new HomePageModel()
            {
                News = news,
                Supporters = supportersRandom
            };

            return View(model);
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Supporters()
        {

            return View();
        }




    }
}
