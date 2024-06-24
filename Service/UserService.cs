using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }

        public void CreateUser(User user)
        { 
            user.IsDelete = false; 
            userRepository.CreateUser(user);
        }
         

        public void DeleteUser(User user)
		{
			userRepository.DeleteUser(user);
		}

        public List<Role> GetRoles()
        {
            return userRepository.GetRoles();
        }

        public User GetUserByID(int id)
		{
			return userRepository.GetUserByID(id);
		}

		public List<User> GetUsers()
        {
            return userRepository.GetAll();
        }

		public List<User> SearchUser(string name, string email)
		{
			return userRepository.SearchUser(name, email);
		}

		public void UpdateUser(User user)
		{
			userRepository.UpdateUser(user);
		}

		public User ValidateUser(string email, string password)
		{
			return userRepository.CheckLogin(email, password);
		}
	}
}
