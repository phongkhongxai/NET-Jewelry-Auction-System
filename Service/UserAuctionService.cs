using BusinessObjects;
using DataAccessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserAuctionService : IUserAuctionService
    {
        private readonly IUserAuctionRepository _userAuctionRepository;
        public UserAuctionService()
        {
			_userAuctionRepository = new UserAuctionRepository();
		}
        public void CreateAuction(UserAuction userAuction) => _userAuctionRepository.CreateAuction(userAuction);

        public List<UserAuction> GetAllUserAuction() => _userAuctionRepository.GetAllUserAuction();

        public List<UserAuction> GetUserAuctionByAuctionId(int id) => _userAuctionRepository.GetUserAuctionByAuctionId(id);

        public List<UserAuction> GetUserAuctionByUserId(int id) => _userAuctionRepository.GetUserAuctionByUserId(id);
    }
}
