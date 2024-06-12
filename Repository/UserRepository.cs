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
		public User GetUserByID(int id)
		{
			return UserDAO.GetUserById(id);
		}

		public void CreateUser(User user)
		{
			UserDAO.CreateUser(user);
		}

		public void UpdateUser(User user)
		{
			UserDAO.UpdateUser(user);
		}

		public void DeleteUser(User user)
		{
			UserDAO.DeleteUser(user);
		}

		public List<User> SearchUser(string name, string email)
		{
			return UserDAO.SearchUser(name, email);
		}
	}
}
