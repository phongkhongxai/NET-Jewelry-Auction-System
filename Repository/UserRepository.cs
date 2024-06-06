using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public User CheckLogin(string email, string password)
        {
            return UserDAO.CheckLogin(email, password);
        }

        public List<User> GetAll()
        {
            return UserDAO.GetUsers();
        }
    }
}
