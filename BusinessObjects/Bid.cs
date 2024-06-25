using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Auction")]
        public int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime BidTime { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
