using Domain.Common;

namespace Domain.Entities
{
    public class Estudiante : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Legajo { get; set; }
        public Guid CareerId { get; set; }
        public Career Career { get; set; }
        public ICollection<EstudianteSubject> EstudianteSubjects { get; set; }
    }
}
