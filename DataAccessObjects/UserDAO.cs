using BusinessObjects;
using Microsoft.EntityFrameworkCore;
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
            return db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email && u.Password == password && !u.IsDelete);
        }

        public static List<User> GetUsers()
        {
            var list = new List<User>();
            try
            {
                using var db = new GroupProjectPRN221();
                list = db.Users.Include(u => u.Role).Where(u  => !u.IsDelete).ToList();
            } catch (Exception ex){
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static User GetUserById(int id)
        {
            using var db = new GroupProjectPRN221();
            return db.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id && !u.IsDelete);
        }
 

        public static void CreateUser(User user)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                db.Users.Add(user);
                db.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception("Error creating user: " + ex.Message);
            }
        }
        public static void UpdateUser(User user)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                db.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }
        public static void DeleteUser(User user) 
        {
            try
            {
                using var db = new GroupProjectPRN221();
                var exist = db.Users.FirstOrDefault(u => u.Id == user.Id);
                if (exist != null)
                {
                    exist.IsDelete = true;
                    db.SaveChanges();
                } else
                {
                    throw new Exception("User not found");
                }
            } catch (Exception ex)
            {
                throw new Exception("Error deleting user: " + ex.Message);
            }
        }

		public static List<User> SearchUser(string name, string email)
		{
			try
			{
				using var db = new GroupProjectPRN221();
				var query = db.Users.ToList();

				if (!string.IsNullOrEmpty(name))
				{
					query = query.Where(u => u.Name.ToLower().Contains(name.ToLower())).ToList();
				}
				if (!string.IsNullOrEmpty(email))
				{
					query = query.Where(u => u.Email.ToLower().Contains(email.ToLower())).ToList();
				}

				query = query.Where(u => !u.IsDelete).ToList();

				return query;
			}
			catch (Exception ex)
			{
				throw new Exception("Error searching users: " + ex.Message);
			}
		}

        public static List<Role> GetAllRoles()
        {
            try
            {
                using var db = new GroupProjectPRN221();
                return db.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting roles: " + ex.Message);
            }
        }
    }
}
