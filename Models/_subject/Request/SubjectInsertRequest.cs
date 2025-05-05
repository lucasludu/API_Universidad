namespace Models._subject.Request
{
    public class SubjectInsertRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CareerId { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
    }
}
