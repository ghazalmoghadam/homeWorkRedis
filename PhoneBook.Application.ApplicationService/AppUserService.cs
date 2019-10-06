using PhoneBook.Domain.DomainModel.Models;
using PhoneBook.Domain.DomainService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Application.ApplicationService
{
    public interface IAppUserService
    {
        User Add(User user);
        void Update(User user);
        void Delete(int id);
        User GetBy(int id);
        IEnumerable<User> GetAll();
        User GetBy(string userName, string password);
    }

    public class AppUserService : IAppUserService
    {
        private readonly IUserService _userService;
        public AppUserService(IUserService userService)
        {
            _userService = userService;
        }

        public User Add(User user)
        {
            return _userService.Add(user);
        }

        public void Delete(int id)
        {
            _userService.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userService.GetAll();
        }

        public User GetBy(int id)
        {
            return _userService.GetBy(id);
        }

        public User GetBy(string userName, string password)
        {
            return _userService.GetBy(userName, password);
        }

        public void Update(User user)
        {
            _userService.Update(user);
        }
    }
}
