using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class WorkSchedule
    {
        [Key]
        public int Id { get; set; }

        // 0 = Ma, 1 = Di, 2 = Wo, 3 = Do, 4 = Vr
        public int DayIndex { get; set; }

        public int Hours { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}