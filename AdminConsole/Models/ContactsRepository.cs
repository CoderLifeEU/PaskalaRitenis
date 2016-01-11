using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConsole.Models
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

        public ContactsModel GetContactById(int contactId)
        {
            var query = from u in _dataContext.Kontakts
                        where u.Id == contactId
                        select u;
            var contact = query.FirstOrDefault();
            var model = new ContactsModel()
            {
                Id = contactId,
                Vieta = contact.NorisesVieta,
                Adrese = contact.Adrese,
                ExtraInfo = contact.PapildusInfo
            };
            return model;
        }

        public void UpdateContact(ContactsModel contact)
        {
            Kontakti contactData = _dataContext.Kontakts.Where(u => u.Id == contact.Id).SingleOrDefault();
            contactData.NorisesVieta = contact.Vieta;
            contactData.Adrese = contact.Adrese;
            contactData.PapildusInfo = contact.ExtraInfo;
            _dataContext.SubmitChanges();
        } 
    }
}