using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress] 
        public string Email { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        public DateOnly Dob { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
         
        [MaxLength(10)]
        public string Gender { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsDelete { get; set; } = false;
        public virtual ICollection<UserAuction> UserAuctions { get; set; } = new List<UserAuction>();

        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
