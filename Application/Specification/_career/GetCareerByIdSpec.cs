using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification._career
{
    public class GetCareerByIdSpec : Specification<Career>
    {
        public GetCareerByIdSpec(int id)
        {
            Query.Where(c => c.Id == id);
        }
    }
}
