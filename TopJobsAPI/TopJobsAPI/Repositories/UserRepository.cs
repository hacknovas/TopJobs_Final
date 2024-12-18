using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private TopJobContext context;

        public UserRepository()
        {
            context = new TopJobContext();
        }

        public Users Login(string username, string password)
        {
            var user = context.Users.SingleOrDefault(x => x.Username == username);

            if (user.Password == password)
            {
                return user;
            }
            return null;
        }

        public Users Register(Users user)
        {
            if (!(context.Users.Any(x => x.Username == user.Username)))
            {
                var userData = context.Users.Add(user);
                context.SaveChanges();
                return userData;
            }

            return null;
        }

    }
}