using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Domain.DomainModel.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool CanGetReport { get; set; }
        public bool CanWrite { get; set; }

        //public string Claims { get; set; }
    }
    //public class Userclaims
    //{
    //    public List<Dictionary<string,string>> MyProperty { get; set; }
    //}
}
