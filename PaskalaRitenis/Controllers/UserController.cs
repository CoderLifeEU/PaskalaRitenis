using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PaskalaRitenis.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        //
        // GET: /User/

        private IUserRepository _repository;

        public UserController()
            : this(new UserRepository())
        {
        }

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }


        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers(DataTablesParams model)
        {
            DataTablesContext<PaskalaRitenis.Models.User> result = new DataTablesContext<PaskalaRitenis.Models.User>();

            model.Search = Request.QueryString["Search[value]"].ToString();

            result.data = _repository.GetUsers(model.Start, model.Length, model.Search);
            result.recordsTotal = _repository.CountUsers(model.Search);
            result.recordsFiltered = result.recordsTotal;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                PaskalaRitenis.Models.User user = new PaskalaRitenis.Models.User();
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Role = (int)Role.Administrator;
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "md5");
                if(_repository.Save(user)) return RedirectToAction("Index");
                else return View(model);
            }
            return View(model);


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PaskalaRitenis.Models.User user = _repository.GetUser(id);
            PasswordViewModel model = new PasswordViewModel(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(model.ID, model.Password);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
