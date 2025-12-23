namespace Application.DTOs._career_subject.Request
{
    public class CareerSubjectRequest
    {
        public string SubjectName { get; set; }
        public string? SubjectDescription { get; set; }
        public string CareerName { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
    }
}
