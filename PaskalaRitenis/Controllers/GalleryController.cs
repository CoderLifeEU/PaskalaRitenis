using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Controllers
{
    public class GalleryController : Controller
    {

        private IVideoRepository _repository;

        public GalleryController() :this(new VideoRepository())
        {

        }

        public GalleryController(IVideoRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Foto()
        {
            return View();
        }

        public ActionResult Video()
        {
            var data = _repository.GetAllVideo()
                .OrderBy(s => s.Gads);

            return View(data);
        }

    }
}
