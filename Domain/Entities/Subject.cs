using Domain.Common;

namespace Domain.Entities
{
    public class Subject : SimpleBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<CareerSubject> CareerSubjects { get; set; }
    }
}
