using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        public User ValidateUser(string email, string password);
        public List<User> GetUsers();
		public User GetUserByID(int id);
		public void CreateUser(User user);
        
        public void UpdateUser(User user);
		public void DeleteUser(User user);
		public List<User> SearchUser(string name, string email);
        public List<Role> GetRoles();   
	}
}
