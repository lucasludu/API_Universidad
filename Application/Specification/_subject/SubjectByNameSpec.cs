using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification._subject
{
    public class SubjectByNameSpec : Specification<Subject>
    {
        public SubjectByNameSpec(string name)
        {
            Query.Where(s => s.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
        }
    }
}
