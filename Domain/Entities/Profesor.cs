using Domain.Common;

namespace Domain.Entities
{
    public class Profesor : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Legajo { get; set; }
        public ICollection<ProfesorSubject> ProfesorSubjects { get; set; }
    }

}
