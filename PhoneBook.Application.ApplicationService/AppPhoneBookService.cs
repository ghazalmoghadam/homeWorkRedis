using PhoneBook.Domain.DomainService;
using PhoneBook.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Application.ApplicationService
{
    public interface IAppPhoneBookService
    {
        Contact Add(Contact contact);
        void Update(Contact contact);
        void Delete(int id);
        Contact GetBy(int id);
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> Search(string searchString);
    }

    public class AppPhoneBookService: IAppPhoneBookService
    {
        private readonly IContactService _contactService;
        public AppPhoneBookService(IContactService contactService)
        {
            _contactService = contactService;
        }

        public Contact Add(Contact contact)
        {
            return _contactService.Add(contact);
        }

        public void Delete(int id)
        {
            _contactService.Delete(id);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contactService.GetAll();
        }

        public Contact GetBy(int id)
        {
            return _contactService.GetBy(id);
        }

        public IEnumerable<Contact> Search(string searchString)
        {
            var contacts = _contactService.GetAll().Where(x => x.Name.Contains(searchString));
            return contacts;
        }

        public void Update(Contact contact)
        {
            _contactService.Update(contact);
        }
    }
}
