using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class UserAuctionDAO
    {
        public static List<UserAuction> GetAllUserAuctions()
        {
            var list = new List<UserAuction>();
            try
            {
                using var db = new GroupProjectPRN221();
                list = db.UserAuctions.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<UserAuction> GetUserAuctionByAuctionsId(int id)
        {
            try
            {
                return new GroupProjectPRN221().UserAuctions.Where(c => c.AuctionId == id).ToList();
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message); 
            }
        }
        public static List<UserAuction> GetUserAuctionByUserId(int id)
        {
            try
            {
                return new GroupProjectPRN221().UserAuctions
                               .Include(ua => ua.Auction).ThenInclude(c=> c.Jewelry)
                               .Where(ua => ua.UserId == id)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void CreateUserAuction(UserAuction userAuction)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                db.UserAuctions.Add(userAuction);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
