using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConsole.Controllers
{
    public class VideoController : Controller
    {
        private IVideoRepository _repository;

        public VideoController() :this(new VideoRepository())
        {

        }
        public VideoController(IVideoRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var data = _repository.GetAllVideo();
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            VideoModel model = _repository.GetVideoById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]   
        public ActionResult Edit(VideoModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateVideo(contact);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(contact);
        }


        public ActionResult Create()
        {
            return View(new VideoModel());
        }

        [HttpPost]
        [ValidateInput(false)]   
        public ActionResult Create(VideoModel video)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.InsertVideo(video);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(video);
        }




        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            VideoModel news = _repository.GetVideoById(id);
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                VideoModel news = _repository.GetVideoById(id);
                _repository.DeleteVideo(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                new System.Web.Routing.RouteValueDictionary { 
          { "id", id }, 
          { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }

    }
}
