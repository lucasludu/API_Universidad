using Domain.Common;

namespace Domain.Entities
{
    public class Career : SimpleBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int DurationInYears { get; set; }
        
        public ICollection<CareerSubject> CareerSubjects { get; set; } 
    }
}
