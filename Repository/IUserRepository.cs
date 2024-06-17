using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        public User CheckLogin(string email, string password);

        public List<User> GetAll();

        public User GetUserByID(int id); 
        public void CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        public List<User> SearchUser(string name, string password);
    }
}
