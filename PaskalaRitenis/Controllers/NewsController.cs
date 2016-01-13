using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Controllers
{
    public class NewsController : Controller
    {

        private INewsRepository _repository;
        public NewsController()
            : this(new NewsRepository())
        {
        }
        public NewsController(INewsRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var news = _repository.GetNews();
            return View(news);
        }

    }
}
