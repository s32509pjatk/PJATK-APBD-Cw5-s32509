using System.ComponentModel.DataAnnotations;

namespace PJATK_APBD_Cw5_s32509.Models
{
    public class Reservation : IValidatableObject
    {
        public int Id { get; set; }
        
        public int RoomId { get; set; }
        
        [Required]
        public string OrganizerName { get; set; }
        
        [Required]
        public string Topic { get; set; }
        
        public DateTime Date { get; set; }
        
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public string Status { get; set; } 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult("EndTime must be later than StartTime.", new[] { nameof(EndTime) });
            }
        }
    }
}