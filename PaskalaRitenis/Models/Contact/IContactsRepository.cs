using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaskalaRitenis.Models.Contact
{
    public interface IContactsRepository
    {
        IEnumerable<ContactsModel> GetContacts();       
    }
}
