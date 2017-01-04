using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Controllers
{
    [Authorize(Roles = "Administrator")]
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
            return View();
        }

        public JsonResult GetNews(DataTablesParams model)
        {
            DataTablesContext<PaskalaRitenis.Models.NewsModel> result = new DataTablesContext<PaskalaRitenis.Models.NewsModel>();

            model.Search = Request.QueryString["Search[value]"].ToString();

            result.data = _repository.GetNews(model.Start, model.Length, model.Search);
            result.recordsTotal = _repository.CountNews(model.Search);
            result.recordsFiltered = result.recordsTotal;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {
            return View(new NewsModel());
        }

        [HttpPost]
        public ActionResult Create(NewsModel news)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    news.Info = System.Net.WebUtility.HtmlDecode(news.Info);
                    _repository.InsertNews(news);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(news);
        }

        public ActionResult Edit(int id)
        {
            NewsModel model = _repository.GetNewsById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(NewsModel news)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    news.Info = System.Net.WebUtility.HtmlDecode(news.Info);
                    _repository.UpdateNews(news);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(news);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            NewsModel news = _repository.GetNewsById(id);
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                NewsModel news = _repository.GetNewsById(id);
                _repository.DeleteNews(id);
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
