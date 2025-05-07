using AutoMapper;
using Domain.Entities;
using Models._career.Request;
using Models._career.Response;
using Models._subject.Request;
using Models._subject.Response;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Career, CareerInsertRequest>().ReverseMap();
            CreateMap<Career, CareerResponse>().ReverseMap();

            CreateMap<Subject, SubjectInsertRequest>().ReverseMap();
            CreateMap<Subject, SubjectResponse>().ReverseMap();
        }
    }
}
