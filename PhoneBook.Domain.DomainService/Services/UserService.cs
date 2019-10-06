using PhoneBook.Domain.DomainModel.Models;
using PhoneBook.Domain.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Domain.DomainService.Services
{
    public interface IUserService
    {
        User Add(User user);
        void Update(User user);
        void Delete(int id);
        User GetBy(int id);
        IEnumerable<User> GetAll();
        User GetBy(string userName, string password);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User Add(User user)
        {
            return _userRepository.Add(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetBy(int id)
        {
            return _userRepository.GetBy(id);
        }

        public User GetBy(string userName, string password)
        {
            return _userRepository.GetBy(userName, password);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }
    }
}
