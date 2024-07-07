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
    public class UserAuctionService: IUserAuctionService
    {
        private readonly IUserAuctionRepository _repository;
        public UserAuctionService()
        {
            _repository = new UserAuctionRepository();
        }
        public void CreateAuction(UserAuction userAuction) => _repository.CreateAuction(userAuction);

        public List<UserAuction> GetAllUserAuction() => _repository.GetAllUserAuction();

        public List<UserAuction> GetUserAuctionByAuctionId(int id) => _repository.GetUserAuctionByAuctionId(id);

        public List<UserAuction> GetUserAuctionByUserId(int id) => _repository.GetUserAuctionByUserId(id);
    }
}
