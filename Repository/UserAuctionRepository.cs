using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserAuctionRepository : IUserAuctionRepository
    {
        public void CreateAuction(UserAuction userAuction) => UserAuctionDAO.CreateUserAuction(userAuction);

        public List<UserAuction> GetAllUserAuction()=> UserAuctionDAO.GetAllUserAuctions();

        public List<UserAuction> GetUserAuctionByAuctionId(int id) => UserAuctionDAO.GetUserAuctionByAuctionsId(id);

        public List<UserAuction> GetUserAuctionByUserId(int id) => UserAuctionDAO.GetUserAuctionByUserId(id);
    }
}
