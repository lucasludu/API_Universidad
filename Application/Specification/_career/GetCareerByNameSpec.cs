using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification._career
{
    public class GetCareerByNameSpec : Specification<Career>
    {
        public GetCareerByNameSpec(string name)
        {
            Query.Where(c => c.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
        }
    }
}
