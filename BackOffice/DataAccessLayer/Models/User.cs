using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Models
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

        public string? UserType { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();

        // New: persisted work schedules (Ma - Vr)
        public ICollection<WorkSchedule> WorkSchedules { get; } = new List<WorkSchedule>();
    }
}