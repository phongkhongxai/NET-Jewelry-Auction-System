using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Jewelry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool IsDelete { get; set; } = false;

        [ForeignKey("AuctionRequest")]
        public int AuctionRequestId { get; set; }
        public virtual AuctionRequest AuctionRequest { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; } = new List<JewelryMaterial>();



    }
}
