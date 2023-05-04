using JWT.NetFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT.NetFramework.Services
{
    public class AuthService
    {
        public static bool Authenticate(string userName, string password)
        {
            var user = UserRepository.Login(userName, password);

            if (user == null)
            {
                return false;
            }

            return user.Password == password;
        }
    }
}