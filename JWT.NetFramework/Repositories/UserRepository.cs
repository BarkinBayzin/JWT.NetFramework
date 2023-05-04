using JWT.NetFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT.NetFramework.Repositories
{
    public class UserRepository
    {
        private static readonly List<User> _users = new List<User>
        { new User { UserName = "bayzin", Password = "123" } };

        public static User GetByUserName(string userName) => _users.FirstOrDefault(u => u.UserName == userName);

        public static User Login(string userName, string password) => _users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
    }
}