using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserAuctionRepository
    {
        public List<UserAuction> GetAllUserAuction();
        public List<UserAuction> GetUserAuctionByAuctionId(int id);
        public void CreateAuction(UserAuction userAuction);
        public List<UserAuction> GetUserAuctionByUserId(int id);
    }
}

