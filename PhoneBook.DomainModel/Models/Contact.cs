using PhoneBook.Domain.DomainModel.Enums;
using System;

namespace PhoneBook.DomainModel
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public GroupEnum Group { get; set; }
        public string Email { get; set; }
    }
   
}
