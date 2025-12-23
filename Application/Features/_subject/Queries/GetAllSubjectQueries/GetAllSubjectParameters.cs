using Application.Parameters;

namespace Application.Features._subject.Queries.GetAllSubjectQueries
{
    public class GetAllSubjectParameters : RequestParameters
    {
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
