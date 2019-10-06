using PhoneBook.Domain.DomainModel.Models;
using PhoneBook.Infrastructure.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBook.Domain.Repository.Repositories
{
    public interface IUserRepository
    {
        User Add(User user); //Return Data with Id
        void Update(User user);
        void Delete(int id);
        User GetBy(int id);
        IEnumerable<User> GetAll();
        User GetBy(string userName, string password);
    }
    public class UserRepository : IUserRepository
    {
        private readonly PhoneBookDbContext ctx;

        public UserRepository(PhoneBookDbContext phoneBookDbContext)
        {
            this.ctx = phoneBookDbContext;
        }

        public User Add(User user)
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user;
        }

        public void Delete(int id)
        {
            var user = new User
            {
                Id = id
            };
            ctx.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return ctx.Users;
        }

        public User GetBy(int id)
        {
            return ctx.Users.Find(id);
        }

        public User GetBy(string userName, string password)
        {
            return ctx.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
        }

        public void Update(User user)
        {
            SetCurrentValues(ctx.Users.FirstOrDefault(x => x.Id == user.Id), user);
            ctx.SaveChanges();
        }
        private void SetCurrentValues(object oldEntity, object curEntity)
        {
            ctx.Entry(oldEntity).CurrentValues.SetValues(curEntity);
        }
    }
}
