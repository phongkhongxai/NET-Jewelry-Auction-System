using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class UserDAO
    {
        public static User CheckLogin(string email, string password)
        {
            using var db = new GroupProjectPRN221();
            return db.Users.FirstOrDefault(a => a.Email == email && a.Password == password);
        }

        public static List<User> GetUsers()
        {
            var list = new List<User>();
            try
            {
                using var db = new GroupProjectPRN221();
                list = db.Users.ToList();
            } catch (Exception ex){
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
