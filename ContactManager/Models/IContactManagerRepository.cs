using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public interface IContactManagerRepository
    {
        ContactInfo CreateContact(ContactInfo contactToCreate);
        void DeleteContact(ContactInfo contactToDelete);
        ContactInfo EditContact(ContactInfo contactToUpdate);
        ContactInfo GetContact(int id);
        IEnumerable <ContactInfo> ListContacts();

    }
}