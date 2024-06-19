using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class AuctionRequest
    {
        [Key]
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [ForeignKey("Auction")]
        public int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }

        public bool IsDelete { get; set; } = false;

    }
}
