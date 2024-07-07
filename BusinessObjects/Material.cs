using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; } = new List<JewelryMaterial>();

    }
}
