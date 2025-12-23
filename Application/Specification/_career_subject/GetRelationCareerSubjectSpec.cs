using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification._career_subject
{
    public class GetRelationCareerSubjectSpec : Specification<CareerSubject>
    {
        public GetRelationCareerSubjectSpec(int careerId, int subjectId)
        {
            Query
                .Where(cs => cs.CareerId == careerId && cs.SubjectId == subjectId)
                .Include(cs => cs.Career)
                .Include(cs => cs.Subject);
        }
    }
}
