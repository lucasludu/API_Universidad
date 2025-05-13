using Domain.Common;

namespace Domain.Entities
{
    public class Estudiante : TraceableEntity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Legajo { get; set; }
        public int CareerId { get; set; }
        public Career Career { get; set; }
        public ICollection<EstudianteSubject> EstudianteSubjects { get; set; }
    }
}
