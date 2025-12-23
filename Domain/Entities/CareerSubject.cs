namespace Domain.Entities
{
    public class CareerSubject
    {
        public int CareerId { get; set; }
        public Career Career { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int Year { get; set; }
        public int Semester { get; set; }
    }
}
