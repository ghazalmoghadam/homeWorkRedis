using PhoneBook.DomainModel;
using PhoneBook.Infrastructure.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Domain.Repository
{
    public interface IContactRepository
    {
        Contact Add(Contact contact); //Return Data with Id
        void Update(Contact contact);
        void Delete(int id);
        Contact GetBy(int id);
        IEnumerable<Contact> GetAll();
    }
    public class ContactRepository : IContactRepository
    {
        private readonly PhoneBookDbContext ctx;

        public ContactRepository(PhoneBookDbContext phoneBookDbContext)
        {
            this.ctx = phoneBookDbContext;
        }

        public Contact Add(Contact contact)
        {
            ctx.Contacts.Add(contact);
            ctx.SaveChanges();
            return contact;
        }

        public void Delete(int id)
        {
            var contact = new Contact
            {
                Id = id
        };
            ctx.Contacts.Remove(contact);
        }

        public IEnumerable<Contact> GetAll()
        {
            return ctx.Contacts;
        }

        public Contact GetBy(int id)
        {
            return ctx.Contacts.Find(id);
        }

        public void Update(Contact contact)
        {
            SetCurrentValues(ctx.Contacts.FirstOrDefault(x => x.Id == contact.Id), contact);
            ctx.SaveChanges();
        }
        private void SetCurrentValues(object oldEntity, object curEntity)
        {
           ctx.Entry(oldEntity).CurrentValues.SetValues(curEntity);
        }
    }
}
