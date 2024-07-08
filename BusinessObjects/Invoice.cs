using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Invoice
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
        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
