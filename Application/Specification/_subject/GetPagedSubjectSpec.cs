using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification._subject
{
    public class GetPagedSubjectSpec : Specification<Subject>
    {
        public GetPagedSubjectSpec(int pageSize, int pageNumber, string?name = null, bool? isActive = null, int? year = null, int? semester = null)
        {
            Query
                .Where(c =>
                    (
                        isActive == null || c.IsActive == isActive) &&
                        (string.IsNullOrEmpty(name) || c.Name.ToLower().Contains(name.ToLower())) &&
                        (year == null || c.Year == year) &&
                        (semester == null || c.Semester == semester) 
                    )
                .Skip((pageNumber - 1) * pageSize)
                .Include(s => s.Career)
                .Take(pageSize)
                .OrderBy(c => c.Id);
        }
    }
}
