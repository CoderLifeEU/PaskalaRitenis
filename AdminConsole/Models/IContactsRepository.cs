using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminConsole.Models
{
    public interface IContactsRepository
    {
        IEnumerable<ContactsModel> GetContacts();
        ContactsModel GetContactById(int contactId);
        void UpdateContact(ContactsModel contact);
    }
}
