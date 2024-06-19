using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Auction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Jewelry")]
        public int JewelryId { get; set; }
        public virtual Jewelry Jewelry { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        public bool IsDelete { get; set; } = false;
        public virtual ICollection<UserAuction> UserAuctions { get; set; } = new List<UserAuction>();
        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<AuctionRequest> AuctionRequests { get; set; } = new List<AuctionRequest>();
    }
}
