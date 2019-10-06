using PhoneBook.Domain.Repository;
using PhoneBook.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Domain.DomainService
{
    public interface IContactService
    {
        Contact Add(Contact contact);
        void Update(Contact contact);
        void Delete(int id);
        Contact GetBy(int id);
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> Search(string searchString);
    }
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public Contact Add(Contact contact)
        {
            return _contactRepository.Add(contact);
        }

        public void Delete(int id)
        {
            _contactRepository.Delete(id);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contactRepository.GetAll();
        }

        public Contact GetBy(int id)
        {
            return _contactRepository.GetBy(id);
        }

        public IEnumerable<Contact> Search(string searchString)
        {
           var contacts= _contactRepository.GetAll().Where(x=>x.Name.Contains(searchString));
            return contacts;
        }

        public void Update(Contact contact)
        {
            _contactRepository.Update(contact);
        }
    }
}
