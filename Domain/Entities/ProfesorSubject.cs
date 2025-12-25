namespace Domain.Entities
{
    public class ProfesorSubject
    {
        public Guid ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        public Guid SubjectId { get; set; } 
        public Subject Subject { get; set; }
    }

}
