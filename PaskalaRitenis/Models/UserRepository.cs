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
        List<User> GetUsers(int start,int pageSize,string search);
        int CountUsers(string search);
        bool Save(User user);
        User GetUser(int id);
        bool Delete(int id);
        bool Update(int id, string password);
        void SubmitChanges();
    }

    public class UserRepository: IUserRepository
    {
        private PaskalaRitenisDataContext _datacontext;

        public UserRepository()
        {
            _datacontext = new PaskalaRitenisDataContext();
        }

        public bool Save(User user)
        {
            try
            {
                _datacontext.Users.InsertOnSubmit(user);
                _datacontext.SubmitChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
            
        }

        public User GetUser(string username)
        {
            User user = new User();
            using (var datacontext = new PaskalaRitenisDataContext())
            {
                user = datacontext.Users.Where(x => x.UserName == username).FirstOrDefault();
            }
            return user;
        }

        public User GetUser(int id)
        {
            User user = new User();
            using (var datacontext = new PaskalaRitenisDataContext())
            {
                user = datacontext.Users.Where(x => x.ID== id).FirstOrDefault();
            }
            return user;
        }

        public bool Delete(int id)
        {
            try
            {
                var user = GetUser(id);
                
                _datacontext.Users.DeleteOnSubmit(user);
                _datacontext.SubmitChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
            
        }

        public bool Update(int id, string password)
        {
            try
            {
                if(IDExists(id))
                {
                    User user = GetUser(id); //_datacontext.Users.Where(x => x.ID == id).FirstOrDefault();
                    user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
                    _datacontext.SubmitChanges();
                    return true;

                }
                else return false;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private bool IDExists(int id)
        {
            if (_datacontext.Users.Where(x => x.ID == id).SingleOrDefault() == null) return false;
            else return true;
        }



        public void SubmitChanges()
        {
            _datacontext.SubmitChanges();
        }

        public List<User> GetUsers(int start, int pageSize, string search)
        {
            List<User> users = new List<User>();
            using (var datacontext = new PaskalaRitenisDataContext())
            {
                users = datacontext.Users.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.UserName.Contains(search)).Skip(start).Take(pageSize).ToList();
            }

            return users;
        }

        public int CountUsers(string search)
        {
            int count = 0;
            using (var datacontext = new PaskalaRitenisDataContext())
            {
                count = datacontext.Users.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.UserName.Contains(search)).Count();
            }

            return count;
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