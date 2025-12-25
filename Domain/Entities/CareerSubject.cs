namespace Domain.Entities
{
    public class CareerSubject
    {
        public Guid CareerId { get; set; }
        public Career Career { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int Year { get; set; }
        public int Semester { get; set; }
    }
}
