using Application.Parameters;

namespace Application.Feautures._subject.Queries.GetAllSubjectQueries
{
    public class GetAllSubjectParameters : RequestParameters
    {
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public int? Year { get; set; }
        public int? Semester { get; set; }
    }
}
