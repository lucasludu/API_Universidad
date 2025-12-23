using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification._subject
{
    public class GetPagedSubjectSpec : Specification<Subject>
    {
        public GetPagedSubjectSpec(int pageSize, int pageNumber, string?name = null, bool? isActive = null)
        {
            Query
                .Where(c =>
                    (
                        isActive == null || c.IsActive == isActive) &&
                        (string.IsNullOrEmpty(name) || c.Name.ToLower().Contains(name.ToLower()))
                    )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(c => c.Id);
        }
    }
}
