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

        public List<User> GetUsers()
        {
            return userRepository.GetAll();
        }

        public User ValidateUser(string email, string password)
        {
            return userRepository.CheckLogin(email, password);
        }
    }
}
