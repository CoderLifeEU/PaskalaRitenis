using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PaskalaRitenis.Models
{

    public interface IUserRepository
    {
        User GetUser(string username);
        bool Exists(string username, string password);
    }

    public class UserRepository: IUserRepository
    {
        private PaskalaRitenisDataContext _datacontext;

        public UserRepository()
        {
            _datacontext = new PaskalaRitenisDataContext();
        }

        public User GetUser(string username)
        {
            User user = _datacontext.Users.Where(x => x.UserName == username).FirstOrDefault();
            return user;
        }

        public bool Exists(string username, string password)
        {
            User user = GetUser(username);
            if (user == null) return false;
            if (FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5") == user.Password)
            {
                return true;
            }
            else return false;
        }

        public string GetRole(int role)
        {
            switch(role)
            {
                case (int)Role.Administrator:
                    {
                        return "Administrator" ;
                    }
                case (int)Role.Manager:
                    {
                        return "Manager";
                    }
                case (int)Role.User:
                    {
                        return "User";
                    }
                default:
                    return "";
            }

        }
        public string[] GetRolesForUser(string username)
        {
            var user = GetUser(username);
            if (user == null) return new string[] { };
            else
            {
                string role = GetRole(user.Role);
                return new string[] { role };
            }
                
        }
    }


}