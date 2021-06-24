using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class EntityContactManagerRepository : ContactManager.Models.IContactManagerRepository
    {
        private ContactManagerDBEntities _entities = new ContactManagerDBEntities();

        public ContactInfo GetContact(int id)
        {
            return (from c in _entities.ContactInfoes
                    where c.Id == id
                    select c).FirstOrDefault();
        }

        public IEnumerable <ContactInfo> ListContacts()
        {
            return _entities.ContactInfoes.ToList();
        }

        public ContactInfo CreateContact(ContactInfo contactToCreate)
        { 
            var a =_entities.InsertOrUpdateContacts(contactToCreate.Id, contactToCreate.FirstName, contactToCreate.LastName, contactToCreate.Number, contactToCreate.Type, contactToCreate.Email, contactToCreate.IsActive, contactToCreate.IsDeleted);
            _entities.SaveChanges();
            return contactToCreate;
        }

        public ContactInfo EditContact(ContactInfo contactToEdit)
        {
            _entities.InsertOrUpdateContacts(contactToEdit.Id, contactToEdit.FirstName, contactToEdit.LastName, contactToEdit.Number, contactToEdit.Type, contactToEdit.Email, contactToEdit.IsActive, contactToEdit.IsDeleted);
            _entities.SaveChanges();
            return contactToEdit;
        }

        public void DeleteContact(ContactInfo contactToDelete)
        {
            var originalContact = GetContact(contactToDelete.Id);
            _entities.ContactInfoes.Remove(originalContact);
            _entities.SaveChanges();
        }

    }
}