using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            //var t1 = Request.QueryString["search"].ToArray();
            model.Search = Request.QueryString["Search[value]"].ToString();

            result.data = _repository.GetUsers(model.Start,model.Length,model.Search);
            result.recordsTotal = _repository.CountUsers(model.Search);
            result.recordsFiltered = result.recordsTotal;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }


}
