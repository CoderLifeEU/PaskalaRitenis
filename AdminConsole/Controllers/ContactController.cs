using AdminConsole.Models;
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

        public ActionResult Edit(int id)
        {
            ContactsModel model = _repository.GetContactById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ContactsModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateContact(contact);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(contact);
        }

    }
}
