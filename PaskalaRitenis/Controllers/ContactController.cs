using PaskalaRitenis.Models.Contact;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConsole.Controllers
{
    public class ContactController : Controller
    {
        private IContactsRepository _repository;
        public ContactController()
            :this(new ContactsRepository())
        {

        }
        public ContactController(IContactsRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var contacts = _repository.GetContacts();
            return View(contacts);
        }
    }
}
