using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification._subject
{
    public class SubjectByNameAndCareerIdSpec : Specification<Subject>
    {
        public SubjectByNameAndCareerIdSpec(string name, int careerId)
        {
            Query
                .Where(s => s.Name.ToLower().Trim().Equals(name.ToLower().Trim()) && s.CareerId == careerId)
                .Include(s => s.Career);
        }
    }
}
