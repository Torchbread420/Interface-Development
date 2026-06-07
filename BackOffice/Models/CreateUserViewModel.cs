using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        // HTML date input -> yyyy-MM-dd
        [Required]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; } = string.Empty;

        // optional: store as digits only (repository expects int)
        public string PhoneNumber { get; set; } = string.Empty;

        public string? UserType { get; set; }

        // New: hours for Monday..Friday
        [Range(0, 24)] public int Ma { get; set; } = 0;
        [Range(0, 24)] public int Di { get; set; } = 0;
        [Range(0, 24)] public int Wo { get; set; } = 0;
        [Range(0, 24)] public int Do { get; set; } = 0;
        [Range(0, 24)] public int Vr { get; set; } = 0;
    }
}