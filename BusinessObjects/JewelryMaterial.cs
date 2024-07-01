using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class JewelryMaterial
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Jewelry")]
        public int JewelryId { get; set; }
        public virtual Jewelry Jewelry { get; set; }

        [ForeignKey("Material")]
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        public decimal Quantity { get; set; }
    }
}
