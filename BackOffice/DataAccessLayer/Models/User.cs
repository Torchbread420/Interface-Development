using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required] 
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public int PhoneNumber { get; set; }

        public string? UserType { get; set; } // Mocht er later een onderscheid gemaakt worden tussen verschillende soorten gebruikers, zoals beheerders, medewerkers, etc.
        
        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}