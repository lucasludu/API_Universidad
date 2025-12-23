namespace Domain.Entities
{
    public class EstudianteSubject
    {
        public Guid EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        public int SubjectId { get; set; } 
        public Subject Subject { get; set; }

        public int? Calificacion { get; set; }
        public bool EstaAprobado { get; set; }
    }

}
