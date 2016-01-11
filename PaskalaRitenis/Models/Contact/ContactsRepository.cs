using PaskalaRitenis.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaskalaRitenis.Models.Contact
{
    public class ContactsRepository : IContactsRepository
    {
        private ContactsDataContext _dataContext;
        public ContactsRepository()
        { 
           _dataContext = new ContactsDataContext();
        }

        public IEnumerable<ContactsModel> GetContacts()
        {
            IList<ContactsModel> contactList = new List<ContactsModel>();
            var query = from kontakti in _dataContext.Kontakts
                        select kontakti;
            var info = query.ToList();
            foreach (var contactData in info)
            {
                contactList.Add(new ContactsModel()
                {
                    Id = contactData.Id,
                    Vieta = contactData.NorisesVieta,
                    Adrese = contactData.Adrese,
                    ExtraInfo = contactData.PapildusInfo
                });
            }
            return contactList;
        }
    }
}